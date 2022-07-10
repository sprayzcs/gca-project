using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;
using Shared.Data.Shipment;
using ShippingService.Services;

namespace ShippingService.Controllers;

[ApiController]
[Route("")]
public class ShippingController : BaseController
{
    private readonly IShippingService _shippingService;

    public ShippingController(INotificationHandler notificationHandler, IShippingService shippingService) : base(notificationHandler)
    {
        _shippingService = shippingService;
    }


    /// <summary>
    ///     Loads a shipping DTO by the entities id.
    /// </summary>
    /// <param name="shipmentId">Id of the shipment</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no Object with the provided shipment id
    /// </remarks>
    [HttpGet("/{shipmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ShipmentDto>))]
    public async Task<IActionResult> GetShipping(Guid shipmentId, CancellationToken cancellationToken)
    {
        return Result(await _shippingService.GetShipmentByIdAsync(shipmentId, cancellationToken));
    }

    /// <summary>
    ///     Creates a new Shipment. Generates the price and a tracking number for the parcel.
    /// </summary>
    /// <param name="request">Object with all needed information to process the shipment</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - INSUFFICIENT_PERMISSIONS: An unauthorized service (or a user in general) tried to access this endpoint
    ///     - SHIPMENT_ALREADY_EXISTS: There is already a shipment for the provided orderId
    ///     - COULD_NOT_SAVE: Error occured while saving the changes
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ShipmentDto>))]
    public async Task<IActionResult> CreateShipping([FromBody] CreateShipmentDto request, CancellationToken cancellationToken)
    {
        return Result(await _shippingService.CreateShipmentForOrderAsync(request.OrderId, request.CartPrice, cancellationToken));
    }

}
