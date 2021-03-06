using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<Category>>> GetAllAsync();
        Task<IDataResult<Category>> GetByIdAsync(int categoryId);
        Task<IResult> AddAsync(Category category);
        Task<IResult> UpdateAsync(Category category);
        Task<IResult> DeleteAsync(int id);
    }
}
