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

#if DEBUG
    [HttpGet]
    public IActionResult GetSessionId()
    {
        return Result(HttpContext.Session.Id);
    }
#endif

    [HttpGet("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
    public async Task<IActionResult> GetCart()
    {
        return Result(await _cartService.GetCart());
    }

    [HttpPatch("/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
    public async Task<IActionResult> AddItemToCart(Guid productId)
    {
        return Result(await _cartService.AddItemToCart(productId));
    }

    [HttpDelete("/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
    public async Task<IActionResult> RemoveItemFromCart(Guid productId)
    {
        return Result(await _cartService.RemoveItemFromCart(productId));
    }

    [HttpDelete("/")]
    public async Task<IActionResult> ClearCart()
    {
        return Result(await _cartService.ClearCart());
    }
}
