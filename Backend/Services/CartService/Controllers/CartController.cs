using CartService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;
using Shared.Data.Cart;

namespace CartService.Controllers;

[ApiController]
[Route("")]
public class CartController : BaseController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService, INotificationHandler notificationHandler) : base(
        notificationHandler)
    {
        _cartService = cartService;
    }

    /// <summary>
    ///     Loads a cart by its id
    /// </summary>
    /// <param name="cartId">Cart id</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no cart identified by the provided id
    /// </remarks>
    [HttpGet("/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetCart(Guid cartId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.GetCart(cartId, cancellationToken));
    }

    /// <summary>
    ///     Loads the item count of a provided cart
    /// </summary>
    /// <param name="cartId">Cart id</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no cart identified by the provided id
    /// </remarks>
    [HttpGet("/{cartId:guid}/count")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetCartItemCount(Guid cartId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.GetCartItemCount(cartId, cancellationToken));
    }

    /// <summary>
    ///     Creates a new, empty cart
    /// </summary>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - None
    /// </remarks>
    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    public async Task<IActionResult> CreateCart(CancellationToken cancellationToken)
    {
        return Result(await _cartService.CreateCart(cancellationToken));
    }

    /// <summary>
    ///     Adds a single item to a cart
    /// </summary>
    /// <param name="cartId">Cart to add the item to</param>
    /// <param name="productId">The product that should be added the cart</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no cart identified by the provided id
    ///     - CART_DEACTIVATED: The cart was already ordered
    ///     - PRODUCT_ALREADY_IN_CART: The product provided in <paramref name="productId" /> is already in the cart
    /// </remarks>
    [HttpPatch("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> AddItemToCart(Guid cartId, Guid productId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.AddItemToCart(cartId, productId, cancellationToken));
    }

    /// <summary>
    ///     Removes a single item from a cart
    /// </summary>
    /// <param name="cartId">Cart to remove the item from</param>
    /// <param name="productId">The product that should be removed from the cart</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no cart identified by the provided id
    ///     - CART_DEACTIVATED: The cart was already ordered
    ///     - PRODUCT_NOT_IN_CART: The product provided in <paramref name="productId" /> is not in the cart
    /// </remarks>
    [HttpDelete("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId,
        CancellationToken cancellationToken)
    {
        return Result(await _cartService.RemoveItemFromCart(cartId, productId, cancellationToken));
    }

    /// <summary>
    ///     Updates the cart as a whole. It is possible to put (or remove) multiple items in the cart with one call.
    ///     The active state can only be changed by other internal services
    /// </summary>
    /// <param name="cartId">Cart to update</param>
    /// <param name="request">The changes in the cart</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no cart identified by the provided id
    ///     - CART_DTO_NULL: The whole object in <paramref name="request" /> is null
    ///     - CART_DEACTIVATED: The cart was already ordered
    ///     - INSUFFICIENT_PERMISSIONS: Caller has not enough permissions to execute the endpoint. The service either has no
    ///     permission, or a user called this endpoint and tries to change the "active" attribute.
    ///     - COULD_NOT_SAVE: Error occured while saving the changes
    /// </remarks>
    [HttpPatch("/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> UpdateCart(Guid cartId, [FromBody] UpdateCartDto request,
        CancellationToken cancellationToken)
    {
        return Result(await _cartService.UpdateCart(cartId, request, cancellationToken));
    }
}
