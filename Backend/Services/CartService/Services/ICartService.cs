using Shared.Data;
using Shared.Data.Cart;

namespace CartService.Services;

public interface ICartService
{
    Task<CartDto> GetCart(Guid cartId, CancellationToken cancellationToken);

    Task<CartDto> AddItemToCart(Guid cartId, Guid productId, CancellationToken cancellationToken);

    Task<CartDto> RemoveItemFromCart(Guid cartId, Guid productId, CancellationToken cancellationToken);

    Task<CartDto> UpdateCart(Guid cartId, UpdateCartDto cartDto, CancellationToken cancellationToken);

    Task<CartDto> CreateCart(CancellationToken cancellationToken);

    Task<int> GetCartItemCount(Guid cartId, CancellationToken cancellationToken);
}
