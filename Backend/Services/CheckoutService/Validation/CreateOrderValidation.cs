using FluentValidation;
using Shared.Data.Checkout;

namespace CheckoutService.Validation;

public class CreateOrderValidation : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidation()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("E-Mail darf nicht leer sein")
            .EmailAddress()
            .WithMessage("E-Mail muss im E-Mail Format sein");

        RuleFor(c => c.Street)
            .NotEmpty()
            .WithMessage("Straße darf nicht leer sein");

        RuleFor(c => c.City)
            .NotEmpty()
            .WithMessage("Stadt darf nicht leer sein");

        RuleFor(c => c.Zipcode)
            .NotEmpty()
            .WithMessage("Postleitzahl darf nicht leer sein")
            .Matches("\\d{5}")
            .WithMessage("Postleitzahl muss eine fünfstellige Zahl sein");

        RuleFor(c => c.Country)
            .NotEmpty()
            .WithMessage("Land darf nicht leer sein");

        RuleFor(c => c.CreditCardNumber)
            .NotEmpty()
            .WithMessage("Kreditkartennummer darf nicht leer sein")
            .Length(16)
            .WithMessage("Kreditkartennummer muss 16 Zeichen lang sein")
            .CreditCard()
            .WithMessage("Kreditkartennummer muss im richtigen Format sein");

        RuleFor(c => c.CreditCardExpiryDate)
            .NotEmpty()
            .WithMessage("Kreditkartenablaufdatum darf nicht leer sein");

        RuleFor(c => c.CreditCardVerificationValue)
            .NotEmpty()
            .WithMessage("CVV darf nicht leer sein")
            .Matches("\\d{3}")
            .WithMessage("CVV muss eine dreistellige Zahl sein");
    }
}
