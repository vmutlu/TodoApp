using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Enums;
using ToDo.Core.Utilities.Linq;
using ToDo.Core.Utilities.Results;

namespace ToDo.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        protected TContext Context { get; }
        protected readonly DbSet<TEntity> dbSet;

        protected EfEntityRepositoryBase(TContext dbContext)
        {
            Context = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            Context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { Context.Set<TEntity>().Remove(entity); });
            Context.SaveChanges();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, PaginationQuery paginationQuery = null, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (includeEntities.Length > 0)
                query = query.IncludeMultiple(includeEntities);

            if (paginationQuery != null)
            {
                var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;
                query = query.Skip(skip).Take(paginationQuery.PageSize);
            }

            return query;
        }

        public async Task<PagingResult<TEntity>> GetAllForPagingAsync(int page, string propertyName, bool asc, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            using (var context = new TContext())
            {
                var list = context.Set<TEntity>().AsQueryable();

                if (includeEntities.Length > 0)
                    list = list.IncludeMultiple(includeEntities);

                if (filter != null)
                    list = list.Where(filter).AsQueryable();

                list = asc ? list.AscOrDescOrder(ESort.Asc, propertyName) : list.AscOrDescOrder(ESort.Desc, propertyName);
                int totalCount = list.Count();
                var start = (page - 1) * 10;
                list = list.Skip(start).Take(15);

                return new PagingResult<TEntity>(await list.ToListAsync(), totalCount, true, "");
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);
            if (includeEntities.Length > 0)
                query = query.IncludeMultiple(includeEntities);

            return await query.SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { Context.Set<TEntity>().Update(entity); });
            Context.SaveChanges();
        }
    }
}
