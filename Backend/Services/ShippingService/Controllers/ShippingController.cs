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

    [HttpGet("/{shipmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ShipmentDto>))]
    public async Task<IActionResult> GetShipping(Guid shipmentId, CancellationToken cancellationToken)
    {
        return Result(await _shippingService.GetShipmentByIdAsync(shipmentId, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentDto))]
    public async Task<IActionResult> CreateShipping([FromBody] CreateShipmentDto request, CancellationToken cancellationToken)
    {
        return Result(await _shippingService.CreateShipmentForOrderAsync(request.OrderId, request.CartPrice, cancellationToken));
    }

}
