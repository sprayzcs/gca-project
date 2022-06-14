using AutoMapper;
using CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Data;

namespace CatalogService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly INotificationHandler _notificationHandler;

    public ProductService(IProductRepository repository, IMapper mapper, INotificationHandler notificationHandler)
    {
        _repository = repository;
        _mapper = mapper;
        this._notificationHandler = notificationHandler;
    }

    public async Task<ProductDto> GetProduct(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ICollection<ProductDto>> GetProducts()
    {
        return _mapper.Map<List<ProductDto>>(await _repository.GetAllNoTracking().ToListAsync());
    }
}
