using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IUserOperationClaimService 
    {
        Task<PagingResult<UserOperationClaim>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(UserOperationClaim tEntity);
        Task<IResult> UpdateAsync(UserOperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
