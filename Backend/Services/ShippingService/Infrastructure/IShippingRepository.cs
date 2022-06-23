using Shared.Data;
using Shared.Infrastructure;
using ShippingService.Data;

namespace ShippingService.Infrastructure;

public interface IShippingRepository : IRepository<Shipment>
{
    Task<Shipment?> GetByCartIdAsync(Guid cartId);
}
