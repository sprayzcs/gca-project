using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;

namespace CatalogService.Controllers;

[ApiController]
[Route("")]
public class CatalogController : BaseController
{
    private readonly IProductService _service;

    public CatalogController(INotificationHandler notificationHandler, IProductService service) : base(notificationHandler)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<IEnumerable<ProductDto>>))]
    public async Task<IActionResult> GetItems()
    {
        return Result(await _service.GetProducts());
    }

    [HttpGet("/list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<IEnumerable<ProductDto>>))]
    public async Task<IActionResult> GetItemsByIds([FromQuery] IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        return Result(await _service.GetProductsByIdsAsync(productIds, cancellationToken));
    }

    [HttpGet]
    [Route("/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetItem([FromRoute] Guid id)
    {
        return Result(await _service.GetProduct(id));
    }
}
