using CheckoutService.Data;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Model;

public class CheckoutContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public CheckoutContext(DbContextOptions options) : base(options)
    {
    }
}
