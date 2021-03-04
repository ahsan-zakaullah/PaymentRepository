using System;
using Newtonsoft.Json;

namespace Payment.Models.Dto_s
{
    public class PaymentDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty("creditCardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CreditCardNumber { get; set; }
        [JsonProperty("cardHolder", NullValueHandling = NullValueHandling.Ignore)]
        public string CardHolder { get; set; }
        [JsonProperty("expirationDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ExpirationDate { get; set; }
        [JsonProperty("securityCode", NullValueHandling = NullValueHandling.Ignore)]
        public int SecurityCode { get; set; }
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }
        [JsonProperty("paymentStateDto", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentStateDto PaymentStateDto { get; set; }
    }
    public class PaymentStateDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty("paymentId", NullValueHandling = NullValueHandling.Ignore)]
        public int PaymentId { get; set; }
        [JsonProperty("paymentStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int PaymentStatus { get; set; }
    }
}
