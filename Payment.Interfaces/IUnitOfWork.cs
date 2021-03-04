using System.Threading.Tasks;
using Payment.Interfaces.IRepositories;
using Payment.Interfaces.IServices;

namespace Payment.Interfaces
{
    public interface IUnitOfWork
    {
        IPaymentRepository Payments { get; }
        ICheapPaymentGateway CheapPaymentGateways { get; }
        IExpensivePaymentGateway ExpensivePaymentGateways { get; }
        IPremiumPaymentGateway PremiumPaymentGateways { get; }
        Task<int> Complete();
    }
}
