using CatalogService.Data;
using CatalogService.Model;
using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Infrastructure;

namespace CatalogService.Infrastructure;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
}
