using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer
{
    public class CreateIndividualCustomerCommand : IRequest<CreateIndividualCustomerDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; } 

        public class CreateIndividualCustomerCommandHandler :
            IRequestHandler<CreateIndividualCustomerCommand, CreateIndividualCustomerDto>
        {
            IIndividualCustomerRepository _individualCustomerRepository;
            IMapper _mapper;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;

            public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository,
                IMapper mapper, IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _individualCustomerRepository = individualCustomerRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<CreateIndividualCustomerDto> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                var mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);

                var createdIndividualCustomer = await _individualCustomerRepository
                    .AddAsync(mappedIndividualCustomer);
                var indCustomerToReturn = _mapper.Map<CreateIndividualCustomerDto>(createdIndividualCustomer);
                return indCustomerToReturn;
            }
        }
    }
}
