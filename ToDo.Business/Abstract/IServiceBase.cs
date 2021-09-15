using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;

namespace ToDo.Business.Abstract
{
    public interface IServiceBase<T, in TKey> where T : class, IEntity, new() where TKey : IEquatable<TKey>
    {
        Task<IDataResult<List<T>>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<T>> GetByIdAsync(TKey id);
        Task<IResult> AddAsync(T tEntity);
        Task<IResult> UpdateAsync(T tEntity);
        Task<IResult> DeleteAsync(TKey id);
    }
}
