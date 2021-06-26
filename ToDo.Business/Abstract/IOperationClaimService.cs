using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<IDataResult<List<OperationClaim>>> GetAllAsync();
        Task<IDataResult<OperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(OperationClaim operation);
        Task<IResult> UpdateAsync(OperationClaim operation);
        Task<IResult> DeleteAsync(int id);
    }
}
