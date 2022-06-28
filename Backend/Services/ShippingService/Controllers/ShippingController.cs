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

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentDto))]
    public async Task<IActionResult> GetShipping(Guid orderId)
    {
        return Result(await _shippingService.GetShipmentByOrderIdAsync(orderId));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentDto))]
    public async Task<IActionResult> CreateShipping([FromBody] CreateShipmentDto request)
    {
        return Result(await _shippingService.CreateShipmentForOrderAsync(request.OrderId, request.CartPrice));
    }
    
}
