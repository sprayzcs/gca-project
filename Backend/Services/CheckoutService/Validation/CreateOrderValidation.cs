using FluentValidation;
using Shared.Data.Checkout;

namespace CheckoutService.Validation;

public class CreateOrderValidation : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidation()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithErrorCode("ORDER_EMAIL_EMPTY")
            .EmailAddress()
            .WithErrorCode("ORDER_EMAIL_WRONG_FORMAT");

        RuleFor(c => c.Street)
            .NotEmpty()
            .WithErrorCode("ORDER_STREET_EMPTY");

        RuleFor(c => c.City)
            .NotEmpty()
            .WithErrorCode("ORDER_CITY_EMPTY");

        RuleFor(c => c.Zipcode)
            .NotEmpty()
            .WithErrorCode("ORDER_ZIPCODE_EMPTY")
            .Matches("\\d{5}")
            .WithErrorCode("ORDER_ZIPCODE_WRONG_FORMAT");

        RuleFor(c => c.Country)
            .NotEmpty()
            .WithErrorCode("ORDER_COUNTRY_EMPTY");

        RuleFor(c => c.CreditCardNumber)
            .NotEmpty()
            .WithErrorCode("ORDER_CREDIT_CARD_NUMBER_EMPTY")
            .Length(16)
            .WithErrorCode("ORDER_CREDIT_CARD_NUMBER_WRONG_LENGTH")
            .CreditCard()
            .WithErrorCode("ORDER_CREDIT_CARD_NUMBER_WRONG_FORMAT");

        RuleFor(c => c.CreditCardExpiryDate)
            .NotEmpty()
            .WithErrorCode("ORDER_CREDIT_CARD_EXPIRY_EMPTY");

        RuleFor(c => c.CreditCardVerificationValue)
            .NotEmpty()
            .WithErrorCode("ORDER_CVV_EMPTY")
            .Matches("\\d{3}")
            .WithErrorCode("ORDER_CVV_WRONG_FORMAT");
    }
}
