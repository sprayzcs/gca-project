using CheckoutService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace CheckoutService.Infrastructure;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }

    public Task<bool> HasCartAlreadyBeenOrdered(Guid cartId)
    {
        return _dbSet.AnyAsync(o => o.CartId == cartId);
    }
}
