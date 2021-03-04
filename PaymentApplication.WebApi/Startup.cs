using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payment.Interfaces.ILogging;
using Payment.Models.Dto_s;
using Payment.Repository;
using PaymentApplication.WebApi.Middleware;
using PaymentApplication.WebApi.Validators;

namespace PaymentApplication.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the SQL database with application DB context. Get the Database name from the connection string.
            var connectionString = Configuration.GetConnectionString("PaymentDatabase");
            services.AddDbContext<PaymentDbContext>(opt => opt
                .UseSqlServer(connectionString));
            // Configure the swagger for API's documentation
            services.AddSwaggerGen();
            // Configure the auto mapper with the type of startup file.
            services.AddAutoMapper(typeof(Startup));
            // Allow the cors for cross origin request.
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().
                            AllowAnyMethod().
                            AllowAnyHeader();

                        //builder.WithOrigins("http://localhost:8090", "http://localhost");
                    });
            });
            // Add the fluent validation on the models
            services.AddMvc().AddFluentValidation();
            // Resolve the dependency for validator of create payment model
            services.AddTransient<IValidator<PaymentDto>, PaymentValidator>();
            // Resolve the dependency for repository
            services.AddRepository();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger, PaymentDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add the middleware the handle the exception at global level
            app.ConfigureExceptionHandler(logger);
            // include the routing middleware
            app.UseRouting();
            // include the cors middleware
            app.UseCors();
            // include the authentication middleware
            app.UseAuthorization();
            // use for routing end points
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Check is any migration is pending to make impact on database
            if (db != null && db.Database.GetPendingMigrations().Any())
            {
                db.Database.Migrate();
            }
            // use the swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });
        }
    }
}
