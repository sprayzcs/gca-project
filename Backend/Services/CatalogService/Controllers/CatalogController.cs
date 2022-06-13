using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;
using Shared.Data;

namespace CatalogService.Controllers;

[Route("/catalog/[action]")]
public class CatalogController : BaseController
{
    private readonly IProductService _service;

    public CatalogController(INotificationHandler notificationHandler, IProductService service) : base(notificationHandler)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ProductDto>))]
    public async Task<IActionResult> GetItems()
    {
        return Result(await _service.GetProducts());
    }

    [HttpGet]
    [Route("/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    public async Task<IActionResult> GetItem([FromRoute] Guid id)
    {
        return Result(await _service.GetProduct(id));
    }
}
