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

    public CatalogController(INotificationHandler notificationHandler, IProductService service) : base(
        notificationHandler)
    {
        _service = service;
    }

    /// <summary>
    ///     Loads all products available in the catalog at once
    /// </summary>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - None
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductDto>>))]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Result(await _service.GetProducts(cancellationToken));
    }

    /// <summary>
    ///     Loads all products provided by id in <paramref name="productIds" />. Unknown Ids will be ignored
    /// </summary>
    /// <param name="productIds">Product ids of the product to load</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - None
    /// </remarks>
    [HttpGet("/list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductDto>>))]
    public async Task<IActionResult> GetProductsByIds([FromQuery] IEnumerable<Guid> productIds,
        CancellationToken cancellationToken)
    {
        return Result(await _service.GetProductsByIdsAsync(productIds, cancellationToken));
    }

    /// <summary>
    ///     Loads a product by id
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <remarks>
    ///     Possible errors thrown by this endpoint:
    ///     - OBJECT_NOT_FOUND: There is no product identified by the provided id
    /// </remarks>
    [HttpGet]
    [Route("/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Result(await _service.GetProduct(id, cancellationToken));
    }
}
