using AutoMapper;
using CartService.Data;
using CartService.Infrastructure;
using Shared;
using Shared.Data;
using Shared.Infrastructure;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repository = cartRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CartDto> GetCart()
    {
        var cart = await GetCartFromSession();

        if (cart != null)
        {
            return _mapper.Map<CartDto>(cart);
        }

        cart = new Cart(Guid.NewGuid());

        cart = await _repository.AddAsync(cart);

        // Set data to session to prevent session reload on every api call (see https://stackoverflow.com/a/57333280/9063611)
        _httpContextAccessor.HttpContext!.Session.Set("cartId", cart.Id.ToByteArray());

        if (!await _unitOfWork.CommitAsync())
        {
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    private async Task<Cart?> GetCartFromSession()
    {
        byte[]? sessionCartId = _httpContextAccessor.HttpContext!.Session.Get("cartId");

        Cart? cart = null;

        if (sessionCartId != null)
        {
            Guid cartId = new Guid(sessionCartId);
            cart = await _repository.GetByIdAsync(cartId);
        }

        return cart;
    }
}
