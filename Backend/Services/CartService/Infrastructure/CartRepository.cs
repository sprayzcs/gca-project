using CartService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace CartService.Infrastructure;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(DbContext context) : base(context)
    {
    }

    public async Task<Cart?> GetBySessionIdAsync(string sessionId)
    {
        return await _dbSet
            .Where(c => c.SessionId == sessionId)
            .Include(c => c.Products)
            .FirstOrDefaultAsync();
    }
}
