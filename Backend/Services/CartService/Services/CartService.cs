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

    public CartService(ICartRepository cartRepository,
                       ICartProductRepository cartProductRepository,
                       INotificationHandler notificationHandler,
                       IUnitOfWork unitOfWork,
                       IMapper mapper)
    {
        _repository = cartRepository;
        _cartProductRepository = cartProductRepository;
        _notificationHandler = notificationHandler;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
        var cart = new Cart(Guid.NewGuid());
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

    public async Task<CartDto> UpdateCart(Guid cartId, CartDto cartDto)
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
