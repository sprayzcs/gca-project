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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductDto>>))]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Result(await _service.GetProducts(cancellationToken));
    }

    [HttpGet("/list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductDto>>))]
    public async Task<IActionResult> GetItemsByIds([FromQuery] IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        return Result(await _service.GetProductsByIdsAsync(productIds, cancellationToken));
    }

    [HttpGet]
    [Route("/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Result(await _service.GetProduct(id, cancellationToken));
    }
}
