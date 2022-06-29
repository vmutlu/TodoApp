using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Abstract
{
    public interface ICategoryService
    {
        Task<PagingResult<Category>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<Category>> GetByIdAsync(int id);
        Task<IResult> AddAsync(Category tEntity);
        Task<IResult> UpdateAsync(Category tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
