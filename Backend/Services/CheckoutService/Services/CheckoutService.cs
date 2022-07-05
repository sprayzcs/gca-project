﻿using AutoMapper;
using CheckoutService.Data;
using CheckoutService.Infrastructure;
using Shared;
using Shared.Data;
using Shared.Data.Checkout;
using Shared.Data.Shipment;
using Shared.Infrastructure;

namespace CheckoutService.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationHandler _notificationHandler;
    private readonly IMapper _mapper;

    private readonly HttpClient _cartClient;
    private readonly HttpClient _catalogClient;
    private readonly HttpClient _shippingClient;

    public CheckoutService(IOrderRepository repository,
                           IUnitOfWork unitOfWork,
                           INotificationHandler notificationHandler,
                           IMapper mapper,
                           IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _notificationHandler = notificationHandler;
        _mapper = mapper;

        _cartClient = httpClientFactory.CreateClient(HttpClients.Cart);
        _catalogClient = httpClientFactory.CreateClient(HttpClients.Catalog);
        _shippingClient = httpClientFactory.CreateClient(HttpClients.Shipping);
    }

    public async Task<OrderDto> GetOrderById(Guid orderId)
    {
        var order = await _repository.GetByIdAsync(orderId);

        if(order == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> CreateOrderFromCart(CreateOrderDto createOrderDto)
    {
        var cartId = createOrderDto.CartId;
        if (await _repository.HasCartAlreadyBeenOrdered(cartId))
        {
            _notificationHandler.RaiseError(CheckoutErrors.CartAlreadyOrdered);
            return new();
        }

        var cartResponseModel = await _cartClient.GetFromJsonAsync<ResponseModel<CartDto>>($"/{cartId}");
        if (cartResponseModel == null || !cartResponseModel.Success || cartResponseModel.Data == null)
        {
            _notificationHandler.RaiseError(CheckoutErrors.InvalidCart);
            return new();
        }

        int totalPrice = 0;

        var cart = cartResponseModel.Data;
        foreach (Guid productId in cart.ProductIds)
        {
            var productResponseModel = await _catalogClient.GetFromJsonAsync<ResponseModel<ProductDto>>($"/{productId}");
            if (productResponseModel == null || !productResponseModel.Success || productResponseModel.Data == null)
            {
                _notificationHandler.RaiseError(CheckoutErrors.InvalidProduct);
                return new();
            }
            totalPrice += productResponseModel.Data.Price;
        }

        var order = new Order(Guid.NewGuid())
        {
            CartId = createOrderDto.CartId,
            PaymentInfo = new()
            {
                CreditCardNumber = createOrderDto.CreditCardNumber,
                CreditCardExpiryDate = createOrderDto.CreditCardExpiryDate
            },
            Address = new()
            {
                Country = createOrderDto.Country,
                City = createOrderDto.City,
                Zipcode = createOrderDto.Zipcode,
                Street = createOrderDto.Street
            },
            Date = DateTimeOffset.UtcNow
        };

        order = await _repository.AddAsync(order);

        ResponseModel<ShipmentDto>? shipmentResponseModel = await _shippingClient.PostAsJsonAsync<CreateShipmentDto, ResponseModel<ShipmentDto>>("/", new CreateShipmentDto()
        {
            CartPrice = totalPrice,
            OrderId = order.Id
        });

        if (shipmentResponseModel == null || !shipmentResponseModel.Success || shipmentResponseModel.Data == null)
        {
            _notificationHandler.RaiseError(CheckoutErrors.ShipmentNotCreated);
            return new();
        }

        order.ShipmentId = shipmentResponseModel.Data.Id;

        if (!await _unitOfWork.CommitAsync())
        {
            return new();
        }

        return _mapper.Map<OrderDto>(order);
    }
}
