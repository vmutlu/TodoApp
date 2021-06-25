using System.Threading.Tasks;
using ToDo.Core.Entities.Concrete;

namespace ToDo.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task AddAsync(UserOperationClaim tEntity);
    }
}
