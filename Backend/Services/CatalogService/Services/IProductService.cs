using Shared.Data;

namespace CatalogService.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetProducts();

    Task<ProductDto> GetProduct(Guid id);
    
    Task<List<ProductDto>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
