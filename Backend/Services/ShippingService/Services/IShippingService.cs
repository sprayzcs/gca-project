using Shared.Data;

namespace ShippingService.Services;

public interface IShippingService
{
    Task<ShipmentDto> CreateShipmentForOrderAsync(Guid orderId, int orderPrice);
    Task<ShipmentDto> GetShipmentByOrderIdAsync(Guid orderId);
}
