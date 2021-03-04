using System;
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
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        #region private

        // Create the repository variable to access the repository layer
        private readonly IPaymentRepository _repository;

        #endregion

        #region constructor

        /*Initialize the repositories*/
        public ExpensivePaymentGateway(IPaymentRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region public
        public async Task<PaymentModel> Create(PaymentModel model)
        {
            int retry = 1;
            bool isFailed;
            do
            {
                try
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
                catch (PaymentException)
                {
                    retry -= 1;
                    isFailed = true;
                }
                catch (Exception)
                {
                    retry -= 1;
                    isFailed = true;
                }

            } while (retry != 0);

            if (isFailed)
            {
                throw new PaymentException("Unable to Save the Payment");
            }
            return model;
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
