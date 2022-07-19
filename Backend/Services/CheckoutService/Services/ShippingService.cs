using CheckoutService.Infrastructure;
using Shared;
using Shared.Data;

namespace CheckoutService.Services;

public class ShippingService : IShippingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationHandler _notificationHandler;
    private readonly HttpClient _cartClient;
    private readonly HttpClient _catalogClient;
    private readonly HttpClient _shippingClient;

    public ShippingService(
        IOrderRepository orderRepository,
        IHttpClientFactory clientFactory,
        INotificationHandler notificationHandler)
    {
        _orderRepository = orderRepository;
        _notificationHandler = notificationHandler;

        _cartClient = clientFactory.CreateClient(HttpClients.Cart);
        _catalogClient = clientFactory.CreateClient(HttpClients.Catalog);
        _shippingClient = clientFactory.CreateClient(HttpClients.Shipping);
    }

    public async Task<int> EstimateShippingPrice(Guid cartId)
    {
        var cartResponseModel = await _cartClient.GetFromJsonAsync<ResponseModel<CartDto>>($"/{cartId}");

        if (cartResponseModel == null || !cartResponseModel.Success)
        {
            _notificationHandler.RaiseError(CheckoutErrors.InvalidCart);
            return -1;
        }

        var cart = cartResponseModel.Data;
        var totalPrice = 0;
        foreach (var productId in cart.ProductIds)
        {
            var productResponseModel =
                await _catalogClient.GetFromJsonAsync<ResponseModel<ProductDto>>($"/{productId}");
            if (productResponseModel is not { Success: true } || productResponseModel.Data == null)
            {
                _notificationHandler.RaiseError(CheckoutErrors.InvalidProduct);
                return -1;
            }

            totalPrice += productResponseModel.Data.Price;
        }

        var shippingPrice = await _shippingClient.GetFromJsonAsync<ResponseModel<int>>($"estimate/{totalPrice}");
        if (shippingPrice is not { Success: true })
        {
            _notificationHandler.RaiseError(CheckoutErrors.CouldNotEstimateShipment);
            return -1;
        }

        return shippingPrice.Data;
    }
}
