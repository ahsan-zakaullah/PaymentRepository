using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Models.Domain_Models;

namespace Payment.Interfaces
{
    public interface IPaymentDbContext
    {
        Task<int> SaveChangesAsync();
        DbSet<PaymentModel> Payments { get; set; }
        DbSet<PaymentState> PaymentStates { get; set; }
    }
}
