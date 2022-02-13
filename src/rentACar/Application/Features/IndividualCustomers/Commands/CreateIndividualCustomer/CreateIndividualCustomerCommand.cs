using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
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

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer
{
    public class CreateIndividualCustomerCommand : IRequest<LoginUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NationalId { get; set; } 

        public class CreateIndividualCustomerCommandHandler : IRequestHandler<CreateIndividualCustomerCommand, LoginUserDto>
        {
            private readonly IIndividualCustomerRepository _individualCustomerRepository;
            private readonly IMapper _mapper;
            private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            private readonly IAuthService _authService;

            public CreateIndividualCustomerCommandHandler(
                IIndividualCustomerRepository individualCustomerRepository,
                IMapper mapper,
                IndividualCustomerBusinessRules individualCustomerBusinessRules,
                IAuthService authService)
            {
                _individualCustomerRepository = individualCustomerRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
                _authService = authService;
            }

            public async Task<LoginUserDto> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                await _individualCustomerBusinessRules.CheckIfUserNameTaken(request.UserName);
                await _individualCustomerBusinessRules.CheckIfNationalIdUsed(request.NationalId);
                await _individualCustomerBusinessRules.CheckIfEmailTaken(request.Email);

                var indCustomerToAdd = _mapper.Map<IndividualCustomer>(request);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                indCustomerToAdd.PasswordSalt = passwordSalt;
                indCustomerToAdd.PasswordHash = passwordHash;
                indCustomerToAdd.Status = true;

                var createdIndCustomer = await _individualCustomerRepository
                    .AddAsync(indCustomerToAdd);

                
                var accessToken = await _authService.CreateAccessToken(createdIndCustomer);

                return new LoginUserDto
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
