using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.DataAccess;
using ToDo.Core.Entities.Concrete;

namespace ToDo.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
       Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
