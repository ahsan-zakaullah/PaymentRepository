using System.Linq;
using System.Threading.Tasks;
using Payment.Models.Domain_Models;

namespace Payment.Interfaces.IRepositories
{
    /// <summary>
    /// Payment Repository interface
    /// </summary>
    public interface IPaymentRepository : IBaseRepository<PaymentModel>
    {
       new IQueryable<PaymentModel> GetAllAsync();
       Task<PaymentModel> GetByCreditCardAsync(string creditCardNumber);
    }
}
