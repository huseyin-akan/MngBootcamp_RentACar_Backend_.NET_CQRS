using Application.Features.CreditCardInfos.Dtos;
using Application.Features.Payments.Dtos;
using Application.Features.Payments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<CreatePaymentDto>
    {
        public DateTime PaymentDate { get; set; }
        public double TotalSum { get; set; }
        public int RentalId { get; set; }
        public CreateCreditCardInfosDto CreditCardInfos { get; set; }

        public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentDto>
        {
            IPaymentRepository _paymentRepository;
            IMapper _mapper;
            PaymentBusinessRules _paymentBusinessRules;

            public CreatePaymentCommandHandler(IPaymentRepository paymentRepository,
                IMapper mapper,
                PaymentBusinessRules paymentBusinessRules)
            {
                _paymentRepository = paymentRepository;
                _mapper = mapper;
                _paymentBusinessRules = paymentBusinessRules;
            }

            public async Task<CreatePaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                await _paymentBusinessRules.CheckIfPaymentIsSuccessful(request.CreditCardInfos);

                //TODO: Kredi kart bilgileri kaydedilmesi istenirse veritabanına kaydedilir.

                var mappedPayment = _mapper.Map<Payment>(request);

                var createdPayment = await _paymentRepository.AddAsync(mappedPayment);

                var paymentToReturn = _mapper.Map<CreatePaymentDto>(createdPayment);

                return paymentToReturn;
            }
        }
    }
}
