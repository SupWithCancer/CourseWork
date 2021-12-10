using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EFRepository<TKey, TEntity, TContext> : IRepository<TKey, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EFRepository(TContext context) { Context = context; }

        public TContext Context { get; }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var item = await Context.Set<TEntity>().AddAsync(entity);
            return item.Entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(0);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return Context.Set<TEntity>()
                          .Remove(entity).Entity;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>()
                                .ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await Context.Set<TEntity>()
                                .FindAsync(id);
        }

        public virtual async Task<int> GetCountAsync()
        {
            return await Context.Set<TEntity>()
                                .CountAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                                .CountAsync(predicate);
        }

        public virtual async Task<List<TEntity>> PagingFetchAsync(int startIndex, int count)
        {
            return await Context.Set<TEntity>()
                                .Take(startIndex..count)
                                .ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                                .Where(predicate)
                                .ToListAsync();
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                                .FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<bool> AnyExistingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                                .AnyAsync(predicate);
        }

        public Task SaveAsync() => Context.SaveChangesAsync();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    Context.Dispose();

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}