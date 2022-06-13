using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Infrastructure;

namespace CatalogService.Data;

public class Product : Entity
{
    public Product(Guid id) : base(id) { }

    public string Name { get; set; } = string.Empty;

    public int Price { get; set; }
}

public class ProductSeed
{
    public static readonly List<Product> Data = new List<Product>()
    {
        new Product(Guid.Parse("7cf05588-d2a7-4760-98b1-c464335711a7"))
        {
            Name = "Ibu 400",
            Price = 50
        },
        new Product(Guid.Parse("ae66081f-d5c4-468d-96ef-52dc163cea32"))
        {
            Name = "Ibu 600",
            Price = 75
        },
        new Product(Guid.Parse("669e6e98-752c-4309-98b6-176aa3c2c8cd"))
        {
            Name = "Ibu 800",
            Price = 110
        }
    };
}
