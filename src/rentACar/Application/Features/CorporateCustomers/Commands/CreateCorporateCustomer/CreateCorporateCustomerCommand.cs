using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Users.Dtos;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer
{
    public class CreateCorporateCustomerCommand : IRequest<LoginUserDto>
    {
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand, LoginUserDto>
        {
            private readonly ICorporateCustomerRepository _corporateCustomerRepository;
            private readonly IMapper _mapper;
            private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            private readonly IAuthService _authService;

            public CreateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository,
                IMapper mapper, CorporateCustomerBusinessRules corporateCustomerBusinessRules, IAuthService authService)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _mapper = mapper;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _authService = authService;
            }

            public async Task<LoginUserDto> Handle(CreateCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                await _corporateCustomerBusinessRules.CheckIfUserNameTaken(request.UserName);
                await _corporateCustomerBusinessRules.CheckIfTaxNumberUsed(request.TaxNumber);
                await _corporateCustomerBusinessRules.CheckIfEmailTaken(request.Email);

                var corpCustomerToAdd = _mapper.Map<CorporateCustomer>(request);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                corpCustomerToAdd.PasswordSalt = passwordSalt;
                corpCustomerToAdd.PasswordHash = passwordHash;
                corpCustomerToAdd.Status = true;

                var createdCorpCustomer = await _corporateCustomerRepository.AddAsync(corpCustomerToAdd);

                var accessToken = await _authService.CreateAccessToken(createdCorpCustomer);

                return new LoginUserDto
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
