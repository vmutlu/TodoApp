using System.Threading.Tasks;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;
using ToDo.Core.Utilities.Security.JWT;
using ToDo.Entities.DTOs;

namespace ToDo.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
        Task<IResult> UserExistsAsync(string email);
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
    }
}
