using Shared.Infrastructure;

namespace ShippingService.Data;

public class Shipment : Entity
{
    public Guid OrderId { get; private set; }
    public string TrackingNumber { get; private set; }
    public int Price { get; private set; }

    public bool Fulfilled { get; private set; }
    
    public Shipment(Guid id, Guid orderId, string trackingNumber, int price) : base(id)
    {
        OrderId = orderId;
        TrackingNumber = trackingNumber;
        Price = price;
        Fulfilled = false;
    }
}
