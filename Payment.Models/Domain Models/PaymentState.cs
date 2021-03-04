namespace Payment.Models.Domain_Models
{
    public class PaymentState
    {
        public int Id { get; set; }
        public int PaymentStatus { get; set; }
        public int PaymentId { get; set; }
        public PaymentModel Payment { get; set; }
    }
}
