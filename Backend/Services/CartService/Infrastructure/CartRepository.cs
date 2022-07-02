using CartService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace CartService.Infrastructure;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(DbContext context) : base(context)
    {
    }

    public override Task<Cart?> GetByIdAsync(Guid id)
    {
        return _dbSet.Include(c => c.Products).Where(c => c.Id == id).FirstOrDefaultAsync();
    }
}
