using AutoMapper;
using CartService.Data;
using CartService.Infrastructure;
using Shared;
using Shared.Data;
using Shared.Data.Cart;
using Shared.Infrastructure;
using Shared.Security;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly ICartProductRepository _cartProductRepository;
    private readonly INotificationHandler _notificationHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecuredMethodService _securedMethodService;

    public CartService(ICartRepository cartRepository,
                       ICartProductRepository cartProductRepository,
                       INotificationHandler notificationHandler,
                       IUnitOfWork unitOfWork,
                       IMapper mapper,
                       ISecuredMethodService securedMethodService)
    {
        _repository = cartRepository;
        _cartProductRepository = cartProductRepository;
        _notificationHandler = notificationHandler;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securedMethodService = securedMethodService;
    }

    public async Task<CartDto> GetCart(Guid cartId)
    {
        var cart = await _repository.GetByIdAsync(cartId);

        if (cart == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> CreateCart()
    {
        var cart = new Cart(Guid.NewGuid())
        {
            Active = true
        };

        await _repository.AddAsync(cart);

        if (!await _unitOfWork.CommitAsync())
        {
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> AddItemToCart(Guid cartId, Guid productId)
    {
        Cart? cart = await _repository.GetByIdAsync(cartId);

        if (cart == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        if (!cart.Active)
        {
            _notificationHandler.RaiseError(CartErrors.CartDeactivated);
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

    public async Task<CartDto> RemoveItemFromCart(Guid cartId, Guid productId)
    {
        Cart? cart = await _repository.GetByIdAsync(cartId);

        if (cart == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        if (!cart.Active)
        {
            _notificationHandler.RaiseError(CartErrors.CartDeactivated);
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

    public async Task<CartDto> UpdateCart(Guid cartId, UpdateCartDto cartDto)
    {
        Cart? cart = await _repository.GetByIdAsync(cartId);

        if (cart == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        if (cartDto == null)
        {
            _notificationHandler.RaiseError(CartErrors.CartDtoNull);
            return new();
        }

        if (!cart.Active)
        {
            _notificationHandler.RaiseError(CartErrors.CartDeactivated);
            return new();
        }

        if (cartDto.ProductIds != null)
        {
            var cartProducts = new List<CartProduct>(cart.Products);
            var productIdsToSet = cartDto.ProductIds;

            cart.Products.Clear();
            foreach (Guid productId in productIdsToSet)
            {
                var cartProduct = cartProducts.Where(p => p.ProductId == productId).FirstOrDefault();
                if (cartProduct != null)
                {
                    cart.Products.Add(cartProduct);
                }
                else
                {
                    cartProduct = new CartProduct(Guid.NewGuid())
                    {
                        ProductId = productId
                    };
                    await _cartProductRepository.AddAsync(cartProduct);
                    cart.Products.Add(cartProduct);
                }
            }
        }

        if (cartDto.Active != null)
        {
            if (!_securedMethodService.CanAccess())
            {
                _notificationHandler.RaiseError(GenericErrorCodes.InsufficientPermissions);
                return new();
            }

            cart.Active = cartDto.Active.Value;
        }

        if (!await _unitOfWork.CommitAsync(false))
        {
            return new();
        }

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<int> GetCartItemCount(Guid cartId)
    {
        Cart? cart = await _repository.GetByIdAsync(cartId);
        if (cart == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        return cart.Products.Count;
    }
}
