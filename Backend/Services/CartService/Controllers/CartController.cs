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

    [HttpGet]
    [Route("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
    public async Task<IActionResult> GetCart()
    {
        return Result(await _cartService.GetCart());
    }
}
