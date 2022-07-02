using CartService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace CartService.Infrastructure;

public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
{
    public CartProductRepository(DbContext context) : base(context)
    {
    }
}
