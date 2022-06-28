using CartService.Data;
using Microsoft.EntityFrameworkCore;

namespace CartService.Model;

public class CartContext : DbContext
{
    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartProduct> CartProducts => Set<CartProduct>();

    public CartContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cart");

        base.OnModelCreating(modelBuilder);
    }
}
