namespace Shared.Data;

public class OrderDto
{
    public Guid Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Email { get; set; } = string.Empty;
    
    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Zipcode { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string CreditCardNumber { get; set; } = string.Empty;

    public DateTimeOffset CreditCardExpiryDate { get; set; }

    public Guid CartId { get; set; }

    public Guid? ShipmentId { get; set; }
}
