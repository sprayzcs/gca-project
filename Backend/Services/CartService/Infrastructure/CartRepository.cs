using CartService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace CartService.Infrastructure;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(DbContext context) : base(context)
    {
    }
}
