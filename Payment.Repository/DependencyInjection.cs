using Microsoft.Extensions.DependencyInjection;
using Payment.Interfaces;
using Payment.Interfaces.ILogging;
using Payment.Interfaces.IRepositories;
using Payment.Interfaces.IServices;
using Payment.Logging;
using Payment.Repository.Repositories;
using Payment.Services;

namespace Payment.Repository
{
    // Creating the static class to inject the dependencies
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            // Invoke the DbContext dependency
            services.AddScoped<IPaymentDbContext, PaymentDbContext>();
            // Invoke the repository dependency
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            // Invoke the payment state repository dependency
            services.AddScoped<IPaymentStateRepository, PaymentStateRepository>();
            // Invoke the payment cheap service dependency
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            // Invoke the payment expensive service dependency
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            // Invoke the payment premium service dependency
            services.AddScoped<IPremiumPaymentGateway, PremiumPaymentGateway>();
            // Invoke the UOW dependency
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Invoke the log service dependency
            services.AddScoped<ILog, LogNLog>();

            return services;
        }
    }
}
