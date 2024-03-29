﻿using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        return entity;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public virtual IQueryable<TEntity> GetAllNoTracking()
    {
        return _dbSet.AsNoTracking();
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _dbContext.Dispose();
    }

    public virtual async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return;
        }

        _dbSet.Remove(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }
}
