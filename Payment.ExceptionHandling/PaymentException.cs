using System;

namespace Payment.ExceptionHandling
{
    public class PaymentException : Exception
    {
        public PaymentException(string message)
            : base(message)
        {

        }

    }
}
