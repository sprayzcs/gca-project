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

    public CartController(ICartService cartService, INotificationHandler notificationHandler) : base(notificationHandler)
    {
        _cartService = cartService;
    }

    [HttpGet("/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetCart(Guid cartId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.GetCart(cartId, cancellationToken));
    }

    [HttpGet("/{cartId:guid}/count")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetCartItemCount(Guid cartId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.GetCartItemCount(cartId, cancellationToken));
    }

    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    public async Task<IActionResult> CreateCart(CancellationToken cancellationToken)
    {
        return Result(await _cartService.CreateCart(cancellationToken));
    }

    [HttpPatch("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> AddItemToCart(Guid cartId, Guid productId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.AddItemToCart(cartId, productId, cancellationToken));
    }

    [HttpDelete("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId, CancellationToken cancellationToken)
    {
        return Result(await _cartService.RemoveItemFromCart(cartId, productId, cancellationToken));
    }

    [HttpPatch("/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> UpdateCart(Guid cartId, [FromBody] UpdateCartDto cartDto, CancellationToken cancellationToken)
    {
        return Result(await _cartService.UpdateCart(cartId, cartDto, cancellationToken));
    }
}
