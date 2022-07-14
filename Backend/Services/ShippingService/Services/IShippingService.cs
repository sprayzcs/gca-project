using Shared.Data;

namespace ShippingService.Services;

public interface IShippingService
{
    Task<ShipmentDto> CreateShipmentForOrderAsync(Guid orderId, int orderPrice, CancellationToken cancellationToken);

    Task<ShipmentDto> GetShipmentByIdAsync(Guid shipmentId, CancellationToken cancellationToken);
}
