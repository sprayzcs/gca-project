using Shared.Infrastructure;

namespace ShippingService.Data;

public class Shipment : Entity
{
    public Guid CartId { get; private set; }
    public string TrackingNumber { get; private set; }
    public int Price { get; private set; }

    public bool Fulfilled { get; private set; }
    
    public Shipment(Guid id, Guid cartId, string trackingNumber, int price) : base(id)
    {
        CartId = cartId;
        TrackingNumber = trackingNumber;
        Price = price;
        Fulfilled = false;
    }
}
