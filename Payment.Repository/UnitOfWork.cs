using System.Threading.Tasks;
using Payment.Interfaces;
using Payment.Interfaces.IRepositories;
using Payment.Interfaces.IServices;

namespace Payment.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPaymentDbContext _context;
        // Define property for payment table
        public IPaymentRepository Payments { get; }
        // Define property for cheap payment gateway service
        public ICheapPaymentGateway CheapPaymentGateways { get; }
        // Define property for Expensive payment gateway service
        public IExpensivePaymentGateway ExpensivePaymentGateways { get; }
        // Define property for premium payment gateway service
        public IPremiumPaymentGateway PremiumPaymentGateways { get; }

        public UnitOfWork(IPaymentDbContext iPaymentDbContext,
            IPaymentRepository iPaymentRepository,ICheapPaymentGateway cheapPaymentGateways, IExpensivePaymentGateway expensivePaymentGateways, IPremiumPaymentGateway premiumPaymentGateways)
        {
            _context = iPaymentDbContext;
            Payments = iPaymentRepository;
            CheapPaymentGateways = cheapPaymentGateways;
            ExpensivePaymentGateways = expensivePaymentGateways;
            PremiumPaymentGateways = premiumPaymentGateways;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}