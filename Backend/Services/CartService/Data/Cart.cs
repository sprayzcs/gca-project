using Shared.Infrastructure;

namespace CartService.Data;

public class Cart : Entity
{
    public Cart(Guid id) : base(id)
    {
    }

    public bool Active { get; set; } = false;

    public ICollection<CartProduct> Products { get; set; } = null!;
}
