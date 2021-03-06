using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities.Concrete;

namespace ToDo.Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task AddAsync(User user);
        Task<User> GetByMailAsync(string email);
    }
}
