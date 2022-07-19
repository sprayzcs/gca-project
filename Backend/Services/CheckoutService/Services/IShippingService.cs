namespace CheckoutService.Services;

public interface IShippingService
{
    Task<int> EstimateShippingPrice(Guid cartId);
}
