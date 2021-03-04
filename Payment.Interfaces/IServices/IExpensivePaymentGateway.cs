using System.Threading.Tasks;
using Payment.Models.Domain_Models;

namespace Payment.Interfaces.IServices
{
    /// <summary>
    /// Expensive service interface
    /// </summary>
    public interface IExpensivePaymentGateway
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
