using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Data;

[Owned]
public class PaymentInfo
{
    public string CreditCardNumber { get; set; } = string.Empty;

    public DateTimeOffset CreditCardExpiryDate { get; set; }
}
