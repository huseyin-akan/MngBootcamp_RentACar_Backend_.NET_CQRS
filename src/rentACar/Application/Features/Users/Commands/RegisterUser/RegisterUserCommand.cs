using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand :IRequest<CreateUserDto>
    {
        public UserForRegisterDto RegisterDto { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, CreateUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules userBusinessRules;

            public CreateUserCommandHandler(IUserRepository userRepository,
                IMapper mapper,
                UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<CreateUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {

                var userToAdd = _mapper.Map<User>(request.RegisterDto);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.RegisterDto.Password, out passwordHash, out passwordSalt);
                userToAdd.PasswordSalt = passwordSalt;
                userToAdd.PasswordHash = passwordHash;
                userToAdd.Status = true;

                var createdUser = await _userRepository.AddAsync(userToAdd);
                var userToReturn =  _mapper.Map<CreateUserDto>(createdUser);
                return userToReturn;
            }
        }
    }
}
