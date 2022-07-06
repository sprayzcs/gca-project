using Shared.Data;

namespace CatalogService.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts(CancellationToken cancellationToken);

    Task<ProductDto> GetProduct(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductDto>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
