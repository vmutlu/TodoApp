using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<PagingResult<OperationClaim>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<OperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(OperationClaim tEntity);
        Task<IResult> UpdateAsync(OperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
