using Shared.Data;

namespace ShippingService.Services;

public interface IShippingService
{
    Task<ShipmentDto> CreateShipmentForCartAsync(Guid cartId, int cartPrice);
    Task<ShipmentDto> GetShipmentByCartIdAsync(Guid cartId);
}
