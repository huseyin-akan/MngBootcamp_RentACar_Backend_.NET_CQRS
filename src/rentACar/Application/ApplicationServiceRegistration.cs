using Application.Features.AdditionalServices.Rules;
using Application.Features.Brands.Rules;
using Application.Features.CarDamages.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Rules;
using Application.Features.Maintenenaces.Rules;
using Application.Features.Models.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Payments.Rules;
using Application.Features.PromotionCodes.Rules;
using Application.Features.Rentals.Rules;
using Application.Features.Users.Rules;
using Application.Services.AddtionalServiceService;
using Application.Services.AuthService;
using Application.Services.CarService;
using Application.Services.CustomerServices;
using Application.Services.FindexScoreService;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.PaymentService;
using Application.Services.PosSystemService;
using Application.Services.PromotionCodeService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Transaction;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.Jwt;
using FluentValidation;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly() );
            

            services.AddScoped<BrandBusinessRules>();
            services.AddScoped<ModelBusinessRules>();
            services.AddScoped<CarBusinessRules>();
            services.AddScoped<ColorBusinessRules>();
            services.AddScoped<FuelBusinessRules>();
            services.AddScoped<RentalBusinessRules>();
            services.AddScoped<MaintenanceBusinessRules>();
            services.AddScoped<IndividualCustomerBusinessRules>();
            services.AddScoped<CorporateCustomerBusinessRules>();
            services.AddScoped<CarDamageBusinessRules>();
            services.AddScoped<InvoiceBusinessRules>();
            services.AddScoped<PaymentBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();
            services.AddScoped<AdditionalServiceBusinessRules>();
            services.AddScoped<PromotionCodeBusinessRules>();


            services.AddScoped<IFindexScoreService, FakeFindexScoreServiceAdapter>();
            services.AddScoped<IPosSystemService, FakePosSystemServiceAdapter>();
            services.AddScoped<IIndividualCustomerService, IndividualCustomerService>();
            services.AddScoped<ICorporateCustomerService, CorporateCustomerService>();
            services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPromotionCodeService, PromotionCodeService>();

            services.AddSingleton<IMailService, MailKitMailService>();
            services.AddSingleton<IElasticSearch, ElasticSearchManager>();
            services.AddSingleton<LoggerServiceBase, FileLogger>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));            

            return services;
        }
    }
}
