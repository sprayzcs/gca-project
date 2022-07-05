using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Data;

[Owned]
public class Address
{
    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Zipcode { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;
}
