using AutoMapper;
using CheckoutService.Data;
using Shared.Data;

namespace CheckoutService.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(o => o.Country, o => o.MapFrom(d => d.Address.Country))
            .ForMember(o => o.City, o => o.MapFrom(d => d.Address.City))
            .ForMember(o => o.Zipcode, o => o.MapFrom(d => d.Address.Zipcode))
            .ForMember(o => o.Street, o => o.MapFrom(d => d.Address.Street))

            .ForMember(o => o.CreditCardNumber, o => o.MapFrom(d => d.PaymentInfo.CreditCardNumber))
            .ForMember(o => o.CreditCardExpiryDate, o => o.MapFrom(d => d.PaymentInfo.CreditCardExpiryDate));
    }
}
