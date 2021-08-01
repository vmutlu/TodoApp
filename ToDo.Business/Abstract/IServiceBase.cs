using System;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IServiceBase<T, in TKey> where T : class, IEntity, new() where TKey : IEquatable<TKey>
    {
        Task<IDataResult<PaginationDataResult<T>>> GetAllAsync(PaginationQuery paginationQuery = null);
        Task<IDataResult<T>> GetByIdAsync(TKey id);
        Task<IResult> AddAsync(T tEntity);
        Task<IResult> UpdateAsync(T tEntity);
        Task<IResult> DeleteAsync(TKey id);
    }
}
