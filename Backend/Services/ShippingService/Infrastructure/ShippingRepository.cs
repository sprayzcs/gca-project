using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using ShippingService.Data;

namespace ShippingService.Infrastructure;

public class ShippingRepository : Repository<Shipment>, IShippingRepository
{
    public ShippingRepository(DbContext context) : base(context)
    {
    }

    public Task<Shipment?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return _dbSet.FirstOrDefaultAsync(s => s.OrderId == orderId, cancellationToken);
    }
}
