using Shared.Data;
using Shared.Data.Cart;

namespace CartService.Services;

public interface ICartService
{
    Task<CartDto> GetCart(Guid cartId);

    Task<CartDto> AddItemToCart(Guid cartId, Guid productId);

    Task<CartDto> RemoveItemFromCart(Guid cartId, Guid productId);

    Task<CartDto> UpdateCart(Guid cartId, UpdateCartDto cartDto);

    Task<CartDto> CreateCart();
}
