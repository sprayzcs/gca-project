using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure;

public class Repository<TEntity> : BaseRepository<TEntity> where TEntity : Entity
{
    public Repository(DbContext context) : base(context)
    {
    }
}
