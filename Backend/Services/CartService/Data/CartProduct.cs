using Shared.Infrastructure;

namespace CartService.Data;

public class CartProduct : Entity
{
    public CartProduct(Guid id) : base(id)
    {
    }

    public Guid ProductId { get; set; }

    public Cart Cart { get; set; } = null!;
}
