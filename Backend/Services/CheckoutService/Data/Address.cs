using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Data;

[Owned]
public class Address
{
    [MaxLength(200)]
    public string Country { get; set; } = string.Empty;

    [MaxLength(200)]
    public string City { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Zipcode { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Street { get; set; } = string.Empty;
}
