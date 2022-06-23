using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using ShippingService.Data;

namespace ShippingService.Infrastructure;

public class ShippingRepository : Repository<Shipment>, IShippingRepository
{
    public ShippingRepository(DbContext context) : base(context)
    {
    }

    public Task<Shipment?> GetByCartIdAsync(Guid cartId)
    {
        return _dbSet.FirstOrDefaultAsync(s => s.CartId == cartId);
    }
}
