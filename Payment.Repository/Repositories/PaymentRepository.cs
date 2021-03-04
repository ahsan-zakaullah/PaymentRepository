using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Interfaces;
using Payment.Interfaces.IRepositories;
using Payment.Models.Domain_Models;

namespace Payment.Repository.Repositories
{
    public class PaymentRepository : BaseRepository<PaymentModel>, IPaymentRepository
    {
        #region Constructor
        public PaymentRepository(IPaymentDbContext context)
            : base(context)
        {

        }

        #endregion
        #region Protected
        protected override DbSet<PaymentModel> DbSet => Db.Payments;
        #endregion

        #region public

        /// <summary>
        /// Get all card holders with their payment status
        /// </summary>
        public new IQueryable<PaymentModel> GetAllAsync()
        {
            return DbSet.Include(x => x.PaymentState);
        }

        public Task<PaymentModel> GetByCreditCardAsync(string creditCardNumber)
        {
            return DbSet.Include(x => x.PaymentState).FirstOrDefaultAsync(x => x.CreditCardNumber == creditCardNumber);
        }

        #endregion
    }
}
