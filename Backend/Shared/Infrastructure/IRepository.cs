namespace Shared.Infrastructure;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllNoTracking();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task RemoveAsync(Guid id);

    Task<TEntity> UpdateAsync(TEntity entity);
    void Remove(TEntity entity);
}
