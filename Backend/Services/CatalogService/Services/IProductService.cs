using Shared.Data;

namespace CatalogService.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();

    Task<ProductDto> GetProduct(Guid id);

    Task<IEnumerable<ProductDto>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
