namespace Shared.Data.Checkout;

public class CreateOrderDto
{
    public Guid CartId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Zipcode { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string CreditCardNumber { get; set; } = string.Empty;

    public DateTimeOffset CreditCardExpiryDate { get; set; }

    public string CreditCardVerificationValue { get; set; } = string.Empty;
}
