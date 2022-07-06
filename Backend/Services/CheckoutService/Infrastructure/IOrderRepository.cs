using CheckoutService.Data;
using Shared.Infrastructure;

namespace CheckoutService.Infrastructure;

public interface IOrderRepository : IRepository<Order>
{
    Task<bool> HasCartAlreadyBeenOrdered(Guid cartId, CancellationToken cancellationToken = default);
}
