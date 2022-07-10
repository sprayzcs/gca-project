using CheckoutService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;
using Shared.Data.Checkout;

namespace CheckoutService.Controllers;

[ApiController]
public class CheckoutController : BaseController
{
    private readonly ICheckoutService _checkoutService;
    private readonly IShippingService _shippingService;

    public CheckoutController(ICheckoutService checkoutService, INotificationHandler notificationHandler,
        IShippingService shippingService) : base(notificationHandler)
    {
        _checkoutService = checkoutService;
        _shippingService = shippingService;
    }

    [HttpGet("/{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        return Result(await _checkoutService.GetOrderById(orderId, cancellationToken));
    }

    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<OrderDto>))]
    public async Task<IActionResult> CreateOrderFromCart([FromBody] CreateOrderDto createOrderDto)
    {
        return Result(await _checkoutService.CreateOrderFromCart(createOrderDto));
    }

    [HttpGet("shipping/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<int>))]
    public async Task<IActionResult> EstimateShippingFromCart(Guid cartId)
    {
        return Result(await _shippingService.EstimateShippingPrice(cartId));
    }
}
