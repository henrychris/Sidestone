using Microsoft.EntityFrameworkCore;
using Sidestone.Host.Data.Entities;

namespace Sidestone.Host.Data.Repositories.Base
{
    public class BaseRepository<TEntity>(DataContext context) : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public IQueryable<TEntity> Query()
        {
            return context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<TEntity?> GetByIdOrDefaultAsync(string id, bool withTracking = true, CancellationToken cancellationToken = default)
        {
            return withTracking
                ? await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                : await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().AnyAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
