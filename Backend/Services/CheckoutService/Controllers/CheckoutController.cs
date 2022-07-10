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

    public CheckoutController(ICheckoutService checkoutService, INotificationHandler notificationHandler) : base(notificationHandler)
    {
        _checkoutService = checkoutService;
    }

    /// <summary>
    ///     Loads an order by its id.
    /// </summary>
    /// <param name="orderId">The order id</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no order with the provided id
    /// </remarks>
    [HttpGet("/{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        return Result(await _checkoutService.GetOrderById(orderId, cancellationToken));
    }

    /// <summary>
    ///     Creates the order provided in request, deactivated the corresponding cart and creates the shipment.
    /// </summary>
    /// <param name="request">All information needed to order</param>
    /// <remarks>
    ///     If any of these errors occured (except the last), the order will ***NOT***  be placed <br />
    ///     Possible errors thrown by this endpoint:
    ///     - CART_ALREADY_ORDERED: Cart was already ordered
    ///     - INVALID_CART: Cart could not be retrieved from cart service
    ///     - INVALID_PRODUCT: One product could not be retrieved from catalog service
    ///     - COULD_NOT_DEACTIVATE_CART: Cart could not be deactivated
    ///     - (ShipmentId is null): Order was placed, but the creation of shipment failed
    /// </remarks>
    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<OrderDto>))]
    public async Task<IActionResult> CreateOrderFromCart([FromBody] CreateOrderDto request)
    {
        return Result(await _checkoutService.CreateOrderFromCart(request));
    }
}
