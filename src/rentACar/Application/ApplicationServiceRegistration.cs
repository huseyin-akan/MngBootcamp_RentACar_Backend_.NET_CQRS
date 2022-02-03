using Application.Features.Brands.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.Maintenenaces.Rules;
using Application.Features.Models.Rules;
using Application.Features.Rentals.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly() );
            services.AddMediatR(Assembly.GetExecutingAssembly() );


            services.AddScoped<BrandBusinessRules>();
            services.AddScoped<ModelBusinessRules>();
            services.AddScoped<CarBusinessRules>();
            services.AddScoped<ColorBusinessRules>();
            services.AddScoped<FuelBusinessRules>();
            services.AddScoped<RentalBusinessRules>();
            services.AddScoped<MaintenanceBusinessRules>();

            return services;
        }
    }
}
