using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Dtos;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Rentals.Commands.RentForIndividualCustomer;
using Application.Services.Managers.Abstract;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ComplexFeatures.RentalProcess.Commands
{
    public class CreateICRentalComplexCommand :IRequest<CreateInvoiceDto>
    {
        public RentForIndividualCustomerCommand RentalCommand { get; set; }
        public CreatePaymentCommand PaymentCommand { get; set; }

        public class CreateICRentalComplexCommandHandler
            : IRequestHandler<CreateICRentalComplexCommand, CreateInvoiceDto>
        {
            private readonly IMediator mediator;
            private readonly IModelService modelService;
            private readonly ICarService carService;

            public CreateICRentalComplexCommandHandler(
                IMediator mediator,
                IModelService modelService,
                ICarService carService)
            {
                this.mediator = mediator;
                this.modelService = modelService;
                this.carService = carService;
            }

            public async Task<CreateInvoiceDto> Handle(CreateICRentalComplexCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this.carService.GetCarById(request.RentalCommand.CarId);
                var rentalResult = await this.mediator.Send(request.RentalCommand);

                var calculatedTotalSum = await this.CalculateTotalSum(request.RentalCommand,car);
                //TODO: Check if requested totals sum equals calculated total sum
                
                request.PaymentCommand.RentalId = rentalResult.Id;
                request.PaymentCommand.TotalSum = calculatedTotalSum;
                var paymentResult = await this.mediator.Send(request.PaymentCommand);

                //TODO: Invoice No oluşturan bir Helper sınıfı yazalım.
                CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
                {
                    CustomerId = request.RentalCommand.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = 20220001,
                    RentalId = rentalResult.Id,
                    TotalSum = calculatedTotalSum
                };

                var invoiceResult = await this.mediator.Send(invoiceCommand);

                return invoiceResult;
            }

            private async Task<double> CalculateTotalSum(RentForIndividualCustomerCommand request, Car car)
            {
                var totalDays = (request.ReturnDate.Date - request.RentDate.Date).Days + 1;

                var dailyPrice = await this.modelService.GetDailyPriceById(car.ModelId);
                var totalSum = dailyPrice * totalDays;

                bool differentCities = request.RentCityId != request.ReturnCityId;
                if (differentCities)
                {
                    totalSum += 500;
                }
                return totalSum;
            }
        }
    }
}
