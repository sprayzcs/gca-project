using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data;

public class OrderDto
{
    public Guid Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Zipcode { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string CreditCardNumber { get; set; } = string.Empty;

    public DateOnly CreditCardExpiryDate { get; set; }

    public Guid CardId { get; set; }

    public Guid? ShipmentId { get; set; }
}
