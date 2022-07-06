using AutoMapper;
using CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared;
using Shared.Data;

namespace CatalogService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly INotificationHandler _notificationHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(IProductRepository repository,
                          IMapper mapper,
                          INotificationHandler notificationHandler,
                          IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _mapper = mapper;
        _notificationHandler = notificationHandler;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ProductDto> GetProduct(Guid id)
    {
        var product = _repository.GetByIdAsQueryable(id);

        if (!await product.AnyAsync())
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }


        return await _mapper.ProjectTo<ProductDto>(product, new { baseUrl = GetBaseUrl() }).FirstAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<ProductDto>>(
            await _repository
                .GetAllNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken)
            );
    }

    public async Task<ICollection<ProductDto>> GetProducts()
    {
        return await _mapper.ProjectTo<ProductDto>(_repository.GetAllNoTracking(), new { baseUrl = GetBaseUrl() }).ToListAsync();
    }

    private string GetBaseUrl()
    {
        var request = _httpContextAccessor.HttpContext!.Request;
        return $"{request.Scheme}://{request.Host}";
    }
}
