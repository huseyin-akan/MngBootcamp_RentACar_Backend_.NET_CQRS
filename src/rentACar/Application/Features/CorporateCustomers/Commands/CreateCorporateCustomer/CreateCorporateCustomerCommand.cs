using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer
{
    public class CreateCorporateCustomerCommand : IRequest<CreateCorporateCustomerDto>
    {
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }

        public class CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand, CreateCorporateCustomerDto>
        {
            ICorporateCustomerRepository _corporateCustomerRepository;
            IMapper _mapper;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

            public CreateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository,
                IMapper mapper, CorporateCustomerBusinessRules corporateCustomerBusinessRules)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _mapper = mapper;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            }

            public async Task<CreateCorporateCustomerDto> Handle(CreateCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                var mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);

                var createdCorporateCustomer = await _corporateCustomerRepository.AddAsync(mappedCorporateCustomer);
                var corporateCustomerToReturn = _mapper.Map<CreateCorporateCustomerDto>(createdCorporateCustomer);
                return corporateCustomerToReturn;
            }
        }
    }
}
