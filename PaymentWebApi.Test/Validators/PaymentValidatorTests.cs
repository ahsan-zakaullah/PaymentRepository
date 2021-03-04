using FluentValidation.TestHelper;
using PaymentApplication.WebApi.Validators;
using Xunit;

namespace PaymentWebApi.Test.Validators
{
    public class PaymentValidatorTests
    {
        private readonly PaymentValidator _test;

        public PaymentValidatorTests()
        {
            _test = new PaymentValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void CreditCardNumber_WhenCreditCardNotValid_ShouldHaveValidationError(string creditCardNumber)
        {
            _test.ShouldHaveValidationErrorFor(x => x.CreditCardNumber, creditCardNumber).WithErrorMessage("Please enter the valid Credit card number");
        }

        [Theory]
        [InlineData("")]
        public void CardHolder_WhenCardHolderEmpty_ShouldHaveValidationError(string cardHolder)
        {
            _test.ShouldHaveValidationErrorFor(x => x.CardHolder, cardHolder).WithErrorMessage("Card holder field cannot be empty.");
        }

    }
}
