using CatalogService.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Model;

public class CatalogContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public CatalogContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasData(ProductSeed.Data);
        });

        base.OnModelCreating(modelBuilder);
    }
}
