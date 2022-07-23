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
        },
        new Product(Guid.Parse("c0383405-68c4-4efb-b5c2-19e970a7b5e0"))
        {
            Name = "Grippostad C",
            Price = 990
        },
        new Product(Guid.Parse("615917d5-b26f-4507-ac2b-554af8d888d0"))
        {
            Name = "Elotrans",
            Price = 2299
        },
        new Product(Guid.Parse("dfeff251-ec28-4530-8f81-27260ec7ce34"))
        {
            Name = "Pantoprazol",
            Price = 1099
        },
        new Product(Guid.Parse("b11de4ae-e4c9-464c-a494-2a4a5e866a38"))
        {
            Name = "Calciumfolinat",
            Price = 53445
        },
        new Product(Guid.Parse("01938276-763b-4104-8763-dfd97654e10a"))
        {
            Name = "Marcumar",
            Price = 2539
        },
        new Product(Guid.Parse("063c055d-2284-41d1-82d0-8c3ad77d7546"))
        {
            Name = "Tamoxifen",
            Price = 10954
        },
        new Product(Guid.Parse("320cffdb-579f-4e05-934e-699e30d68fd2"))
        {
            Name = "Vitamin D3",
            Price = 1239
        }
    };
}
