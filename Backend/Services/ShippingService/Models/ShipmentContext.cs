using Microsoft.EntityFrameworkCore;
using ShippingService.Data;

namespace ShippingService.Models;

public class ShipmentContext : DbContext
{

    public DbSet<Shipment> Shipments { get; private set; } = null!;

    public ShipmentContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>().Property(s => s.TrackingNumber).HasMaxLength(16);
        
        base.OnModelCreating(modelBuilder);
    }
}
