using CartService.Data;
using Shared.Infrastructure;

namespace CartService.Infrastructure;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetBySessionIdAsync(string sessionId);
}
