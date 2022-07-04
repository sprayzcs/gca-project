using CartService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;

namespace CartService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
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
    public async Task<IActionResult> GetCart(Guid cartId)
    {
        return Result(await _cartService.GetCart(cartId));
    }

    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    public async Task<IActionResult> CreateCart()
    {
        return Result(await _cartService.CreateCart());
    }

    [HttpPatch("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> AddItemToCart(Guid cartId, Guid productId)
    {
        return Result(await _cartService.AddItemToCart(cartId, productId));
    }

    [HttpDelete("/{cartId:guid}/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId)
    {
        return Result(await _cartService.RemoveItemFromCart(cartId, productId));
    }

    [HttpPatch("/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CartDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> UpdateCart(Guid cartId, [FromBody] CartDto cartDto)
    {
        return Result(await _cartService.UpdateCart(cartId, cartDto));
    }
}
