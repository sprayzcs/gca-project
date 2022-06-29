using Shared.Data;

namespace CartService.Services;

public interface ICartService
{
    Task<CartDto> GetCart();
}
