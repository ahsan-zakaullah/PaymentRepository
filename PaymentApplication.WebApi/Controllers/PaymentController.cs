using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.ExceptionHandling;
using Payment.Interfaces;
using Payment.Models.Domain_Models;
using Payment.Models.Dto_s;

namespace PaymentApplication.WebApi.Controllers
{
    // Set to define the controller from Api controller
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _iUnitOfWork; // Create the i UOW variable to access the UOW layer
        private readonly IMapper _mapper; // Create the mapper variable to apply the mapping using auto mapper

        public PaymentController(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;// Initialize the i UOW variable to access the UOW layer
            _mapper = mapper;// Initialize the mapper variable to apply the mapping using auto mapper
        }

        /// <summary>
        /// Action to create a new Payment in the database.
        /// </summary>
        /// <param name="model">Model to create a new Payment</param>
        /// <returns>Returns the created Payment</returns>
        /// <response code="200">Returned if the Payment was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the Payment couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost("ProcessPayment")]
        public async Task<ActionResult<PaymentModel>> ProcessPayment([FromBody] PaymentDto model)
        {
            try
            {
                // Map the DTO with the domain model for payment.
                var paymentModel = _mapper.Map<PaymentModel>(model);
                // Map the specific DTO field with the domain model for payment state.
                var paymentStateModel = _mapper.Map<PaymentState>(model.PaymentStateDto);
                // set payment state object the the domain payment state
                paymentModel.PaymentState = paymentStateModel;
                // Check the record based on the credit card number and if the record found then need to update the fields accordingly
                var result = await _iUnitOfWork.Payments.GetByCreditCardAsync(paymentModel.CreditCardNumber);
                if (result != null)
                {
                    // Use the cheap payment gateway to update the record
                    if (model.Amount < 20)
                    {
                        // Invoke the UOW for cheap gateway to update the record
                        _iUnitOfWork.CheapPaymentGateways.Update(paymentModel);
                    }
                    // Use the Expensive payment gateway to update the record
                    else if (model.Amount > 20 && model.Amount < 500)
                    {
                        // Invoke the UOW for expensive gateway to update the record
                        _iUnitOfWork.ExpensivePaymentGateways.Update(paymentModel);
                    }
                    // Use the premium payment gateway to update the record
                    else
                    {
                        // Invoke the UOW for premium gateway to update the record
                        _iUnitOfWork.PremiumPaymentGateways.Update(paymentModel);
                    }
                    // Call the complete function of UOW to make impact on the database
                    await _iUnitOfWork.Complete();
                }
                else
                {
                    // Use the cheap payment gateway to save the record
                    if (model.Amount < 20)
                    {
                        // Invoke the UOW for cheap gateway to save the record
                        await _iUnitOfWork.CheapPaymentGateways.Create(paymentModel);
                    }
                    // Use the Expensive payment gateway to create the record
                    else if (model.Amount > 20 && model.Amount < 500)
                    {
                        // Invoke the UOW for expensive gateway to create the record
                        await _iUnitOfWork.ExpensivePaymentGateways.Create(paymentModel);
                    }
                    // Use the premium payment gateway to create the record
                    else
                    {
                        // Invoke the UOW for premium gateway to create the record
                        await _iUnitOfWork.PremiumPaymentGateways.Create(paymentModel);
                    }
                }
                return Ok();
            }
            catch (PaymentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}