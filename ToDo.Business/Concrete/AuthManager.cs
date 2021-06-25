using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Enums;
using ToDo.Core.Utilities.Results;
using ToDo.Core.Utilities.Security.Hashing;
using ToDo.Core.Utilities.Security.JWT;
using ToDo.Entities.DTOs;

namespace ToDo.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimService _userOperationClaim;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperationClaim)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationClaim = userOperationClaim;
        }

        public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await _userService.AddAsync(user);

            await _userOperationClaim.AddAsync(new UserOperationClaim()
            {
                UserId = user.Id,
                OperationClaimId = (int)EOperationsClaims.user // register olan user'a default user yetkisinin verilmesi
            });
            return new SuccessDataResult<User>(user, "Kayıt oldu");
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Parola hatası");
            }

            return new SuccessDataResult<User>(userToCheck, "Başarılı giriş");
        }

        public async Task<IResult> UserExistsAsync(string email)
        {
            if (await _userService.GetByMailAsync(email) != null)
            {
                return new ErrorResult("Kullanıcı mevcut");
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }
    }
}
