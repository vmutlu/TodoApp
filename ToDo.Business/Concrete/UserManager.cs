using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;
using System.Linq;
using ToDo.Core.Extensions.MapHelper;

namespace ToDo.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return await _userDal.GetClaimsAsync(user);
        }

        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        public async Task<User> GetByMailAsync(string email)
        {
            return await _userDal.GetAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = (from uop in await _userDal.GetAllAsync(null, u => u.UserOperationClaims).ConfigureAwait(false)
                         from uoc in uop.UserOperationClaims
                         where uoc.UserId == uop.Id
                         select new User()
                         {
                             Id = uop.Id,
                             FirstName = uop.FirstName,
                             LastName = uop.LastName,
                             Email = uop.Email,
                             Status = uop.Status,
                             UserOperationClaims = uop.UserOperationClaims != null ? new List<UserOperationClaim>()
                                                                                      {
                                                                                           new UserOperationClaim()
                                                                                            {
                                                                                                Id = uop.UserOperationClaims.GetListMapped(i=>i.Id),
                                                                                                UserId = uop.UserOperationClaims.GetListMapped(i=>i.UserId)
                                                                                            }
                                                                                      } : null
                         }).ToList();

            return users;
        }
    }
}
