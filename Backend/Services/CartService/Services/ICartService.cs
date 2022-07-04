using Shared.Data;

namespace CartService.Services;

public interface ICartService
{
    Task<CartDto> GetCart(Guid cartId);

    Task<CartDto> AddItemToCart(Guid cartId, Guid productId);

    Task<CartDto> RemoveItemFromCart(Guid cartId, Guid productId);

    Task<CartDto> UpdateCart(Guid cartId, CartDto cartDto);

    Task<CartDto> CreateCart();

    Task<int> GetCartItemCount(Guid cartId);
}
