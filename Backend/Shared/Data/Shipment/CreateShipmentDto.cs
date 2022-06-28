namespace Shared.Data.Shipment;

public class CreateShipmentDto
{
    public Guid OrderId { get; set; }
    public int CartPrice { get; set; }
}
