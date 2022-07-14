namespace Shared.Infrastructure;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllNoTracking();

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity);
}
