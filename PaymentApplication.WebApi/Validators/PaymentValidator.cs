using System;
using FluentValidation;
using Payment.Models.Dto_s;

namespace PaymentApplication.WebApi.Validators
{
    /// <summary>
    /// Create payment fluent validation
    /// </summary>
    public class PaymentValidator : AbstractValidator<PaymentDto>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.CreditCardNumber)
                .NotEmpty().Matches("^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})$")
                .WithMessage("Please enter the valid Credit card number");
            RuleFor(x => x.CardHolder)
                .NotEmpty()
                .WithMessage("Card holder field cannot be empty.");
            RuleFor(x => x.ExpirationDate)
                .NotEmpty().GreaterThan(DateTime.Now)
                .WithMessage("Expiry date cannot be in the past.");
            RuleFor(x => x.SecurityCode.ToString()).Length(3)
                .WithMessage("Only 3 digit Security Code");
            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("Amount should be greater than 0.");
        }
    }
}
