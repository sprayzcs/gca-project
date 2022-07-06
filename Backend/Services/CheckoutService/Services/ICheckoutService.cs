using Shared.Data;
using Shared.Data.Checkout;

namespace CheckoutService.Services;

public interface ICheckoutService
{
    Task<OrderDto> CreateOrderFromCart(CreateOrderDto createOrderDto);

    Task<OrderDto> GetOrderById(Guid orderId, CancellationToken cancellationToken);
}
