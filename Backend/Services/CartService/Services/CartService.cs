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
    private readonly ICartProductRepository _cartProductRepository;
    private readonly INotificationHandler _notificationHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(ICartRepository cartRepository, ICartProductRepository cartProductRepository, INotificationHandler notificationHandler, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repository = cartRepository;
        _cartProductRepository = cartProductRepository;
        _notificationHandler = notificationHandler;
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

    public async Task<CartDto> AddItemToCart(Guid productId)
    {
        Cart? cart = await GetCartFromSession();

        if (cart == null)
        {
            _notificationHandler.RaiseError(CartErrors.CartNotCreated);
            return new();
        }

        if (cart.Products.Any(p => p.ProductId == productId))
        {
            _notificationHandler.RaiseError(CartErrors.ProductAlreadyInCart);
            return new();
        }

        var cartProduct = new CartProduct(Guid.NewGuid())
        {
            ProductId = productId,
            Cart = cart
        };

        await _cartProductRepository.AddAsync(cartProduct);
        cart.Products.Add(cartProduct);

        if (!await _unitOfWork.CommitAsync())
        {
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> RemoveItemFromCart(Guid productId)
    {
        Cart? cart = await GetCartFromSession();

        if (cart == null)
        {
            _notificationHandler.RaiseError(CartErrors.CartNotCreated);
            return new();
        }

        if (!cart.Products.Any(p => p.ProductId == productId))
        {
            _notificationHandler.RaiseError(CartErrors.ProductNotInCart);
            return new();
        }

        CartProduct cartProduct = cart.Products.Where(p => p.ProductId == productId).First();
        cart.Products.Remove(cartProduct);
        _cartProductRepository.Remove(cartProduct);

        if (!await _unitOfWork.CommitAsync())
        {
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> ClearCart()
    {
        Cart? cart = await GetCartFromSession();

        if (cart == null)
        {
            _notificationHandler.RaiseError(CartErrors.CartNotCreated);
            return new();
        }

        if(!cart.Products.Any())
        {
            _notificationHandler.RaiseError(CartErrors.CartEmpty);
            return new();
        }

        cart.Products.Clear();

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
