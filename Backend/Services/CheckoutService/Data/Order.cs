using Shared.Infrastructure;

namespace CheckoutService.Data;

public class Order : Entity
{
    public Order(Guid id) : base(id)
    {
    }

    public DateTimeOffset Date { get; set; }

    public Guid CartId { get; set; }

    public string Email { get; set; } = string.Empty;

    public Guid? ShipmentId { get; set; }

    public Address Address { get; set; } = new();

    public PaymentInfo PaymentInfo { get; set; } = new();
}
