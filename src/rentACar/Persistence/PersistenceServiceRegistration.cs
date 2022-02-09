using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("RentACarConnectionString") ));
            
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IFuelRepository, FuelRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
            services.AddScoped<ICorporateCustomerRepository, CorporateCustomerRepository>();
            services.AddScoped<ICarDamageRepository, CarDamageRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}
