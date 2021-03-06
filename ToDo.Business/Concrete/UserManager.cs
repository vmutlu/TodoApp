using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;

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
    }
}
