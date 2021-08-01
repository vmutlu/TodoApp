﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;

namespace ToDo.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, PaginationQuery paginationQuery = null, params Expression<Func<T, object>>[] includeEntities);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeEntities);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
