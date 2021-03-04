using Microsoft.EntityFrameworkCore;
using Payment.Interfaces;
using Payment.Interfaces.IRepositories;
using Payment.Models.Domain_Models;

namespace Payment.Repository.Repositories
{
    public class PaymentStateRepository : BaseRepository<PaymentState>, IPaymentStateRepository
    {
        #region Constructor
        public PaymentStateRepository(IPaymentDbContext context)
            : base(context)
        {

        }

        #endregion
        #region Protected
        protected override DbSet<PaymentState> DbSet => Db.PaymentStates;
        #endregion
    }
}
