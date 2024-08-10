using Sidestone.Host.Data.Entities;

namespace Sidestone.Host.Data.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> Query();
        Task<TEntity?> GetByIdOrDefaultAsync(string id, bool withTracking = true, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);
    }
}
