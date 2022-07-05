using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Checkout;

public class CreateOrderDto
{
    public Guid CartId { get; set; }

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Zipcode { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string CreditCardNumber { get; set; } = string.Empty;

    public DateTimeOffset CreditCardExpiryDate { get; set; }
}
