namespace Shared.Data.Shipment;

public class CreateShipmentDto
{
    public Guid CartId { get; set; }
    public int CartPrice { get; set; }
}
