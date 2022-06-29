using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Abstract
{
    public interface ITodoService 
    {
        Task<PagingResult<Todo>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<Todo>> GetByIdAsync(int id);
        Task<IResult> AddAsync(Todo tEntity);
        Task<IResult> UpdateAsync(Todo tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
