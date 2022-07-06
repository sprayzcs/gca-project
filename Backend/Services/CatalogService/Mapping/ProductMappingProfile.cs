using AutoMapper;
using CatalogService.Data;
using Shared.Data;

namespace CatalogService.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        string? baseUrl = null;
        CreateProjection<Product, ProductDto>()
            .ForMember(p => p.Image, o => o.MapFrom(p => $"{baseUrl}/images/{p.Id}.png"));
    }
}
