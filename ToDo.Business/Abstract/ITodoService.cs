using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Abstract
{
    public interface ITodoService
    {
        Task<IDataResult<PaginationDataResult<Todo>>> GetAllAsync(PaginationQuery paginationQuery = null);
        Task<IDataResult<Todo>> GetByIdAsync(int todoId);
        Task<IResult> AddAsync(Todo tEntity);
        Task<IResult> UpdateAsync(Todo tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
