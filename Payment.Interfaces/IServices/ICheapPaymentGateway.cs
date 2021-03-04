using System.Threading.Tasks;
using Payment.Models.Domain_Models;

namespace Payment.Interfaces.IServices
{
    public interface ICheapPaymentGateway
    {
        /// <summary>
        /// Create Payment
        /// </summary>
        Task<PaymentModel> Create(PaymentModel model);

        /// <summary>
        /// update changes
        /// </summary>
        void Update(PaymentModel updateModel);
    }
}
