using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Interfaces;
using Payment.Models.Domain_Models;

namespace Payment.Repository
{
    public class PaymentDbContext : DbContext, IPaymentDbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        { }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Defining the 0ne to one relation between tables
            modelBuilder.Entity<PaymentModel>()
                .HasOne(b => b.PaymentState)
                .WithOne(i => i.Payment)
                .HasForeignKey<PaymentState>(b => b.PaymentId);
            modelBuilder.Entity<PaymentState>().HasKey(x => x.Id);
        }
        // Define data set for payment table
        public DbSet<PaymentModel> Payments { get; set; }
        // Define data set for payment state table
        public DbSet<PaymentState> PaymentStates { get; set; }
    }
}
