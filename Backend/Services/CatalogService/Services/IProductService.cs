using Shared.Data;

namespace CatalogService.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetProducts(CancellationToken cancellationToken);

    Task<ProductDto> GetProduct(Guid id, CancellationToken cancellationToken);
    
    Task<List<ProductDto>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
