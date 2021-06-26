using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<List<UserOperationClaim>>> GetAllAsync();
        Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(UserOperationClaim operation);
        Task<IResult> UpdateAsync(UserOperationClaim operation);
        Task<IResult> DeleteAsync(int id);
    }
}
