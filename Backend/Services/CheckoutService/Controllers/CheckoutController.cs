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
    ///     - COULD_NOT_SAVE: Error occured while saving the changes
    ///     - (ShipmentId is null): Order was placed, but the creation of shipment failed
    /// </remarks>
    [HttpPost("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<OrderDto>))]
    public async Task<IActionResult> CreateOrderFromCart([FromBody] CreateOrderDto request)
    {
        return Result(await _checkoutService.CreateOrderFromCart(request));
    }

    /// <summary>
    ///     Estimates the shipment costs for a cart
    /// </summary>
    /// <param name="cartId">The cartId to calculate the shipping for</param>
    /// <remarks>
    ///     ***Does not create the shipping***
    ///     Possible errors thrown by this endpoint:
    ///     - CART_ALREADY_ORDERED: Cart was already ordered
    ///     - INVALID_CART: Cart could not be retrieved from cart service
    ///     - INVALID_PRODUCT: One product could not be retrieved from catalog service
    ///     - COULD_NOT_ESTIMATE_SHIPMENT: Shipment Service did not provide a valid price
    ///     - COULD_NOT_SAVE: Error occured while saving the changes
    /// </remarks>
    [HttpGet("shipping/{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<int>))]
    public async Task<IActionResult> EstimateShippingFromCart(Guid cartId)
    {
        return Result(await _shippingService.EstimateShippingPrice(cartId));
    }
}
