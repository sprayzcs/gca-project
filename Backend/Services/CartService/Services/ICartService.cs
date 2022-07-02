using Shared.Data;

namespace CartService.Services;

public interface ICartService
{
    Task<CartDto> GetCart();

    Task<CartDto> AddItemToCart(Guid productId);

    Task<CartDto> RemoveItemFromCart(Guid productId);

    Task<CartDto> ClearCart();
}
