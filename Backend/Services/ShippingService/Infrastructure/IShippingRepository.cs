﻿using Shared.Data;
using Shared.Infrastructure;
using ShippingService.Data;

namespace ShippingService.Infrastructure;

public interface IShippingRepository : IRepository<Shipment>
{
    Task<Shipment?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
}
