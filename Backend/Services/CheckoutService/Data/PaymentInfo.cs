using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Data;

[Owned]
public class PaymentInfo
{
    [MaxLength(16)]
    public string CreditCardNumber { get; set; } = string.Empty;

    public DateTimeOffset CreditCardExpiryDate { get; set; }
}
