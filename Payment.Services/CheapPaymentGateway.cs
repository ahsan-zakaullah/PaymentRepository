using System.Threading.Tasks;
using Payment.ExceptionHandling;
using Payment.Interfaces.IRepositories;
using Payment.Interfaces.IServices;
using Payment.Models.Domain_Models;

namespace Payment.Services
{
    /// <summary>
    /// Cheap Payment gateway service inherited from it's interface
    /// </summary>
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        #region private
        // Create the repository variable to access the repository layer
        private readonly IPaymentRepository _repository;

        #endregion

        #region constructor

        /*Initialize the repositories*/
        public CheapPaymentGateway(IPaymentRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region public
        /// <summary>
        /// save changes
        /// </summary>
        public async Task<PaymentModel> Create(PaymentModel model)
        {
            // try to add the payment model
            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();
            // try to add the payment model
            var result = _repository.GetByCreditCardAsync(model.CreditCardNumber);
            if (result != null)
            {
                return model;
            }
            // Throw the custom exception if unable to save the payment
            throw new PaymentException("Unable to Save the Payment");
        }

        /// <summary>
        /// update changes
        /// </summary>
        public void Update(PaymentModel updateModel)
        {
            // update changes for payment table
            _repository.Update(updateModel);
        }

        #endregion
    }
}
