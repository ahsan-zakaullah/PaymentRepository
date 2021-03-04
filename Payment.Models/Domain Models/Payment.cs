using System;

namespace Payment.Models.Domain_Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SecurityCode { get; set; }
        public int Amount { get; set; }
        public PaymentState PaymentState { get; set; }
    }
}
