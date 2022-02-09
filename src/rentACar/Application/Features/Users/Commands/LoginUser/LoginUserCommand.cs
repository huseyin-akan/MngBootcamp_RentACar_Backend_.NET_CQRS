using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Hashing;
using Core.Utilities.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand :IRequest<LoginUserDto>
    {
        public UserForLoginDto LoginDto { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules userBusinessRules;

            public LoginUserCommandHandler(IUserRepository userRepository,
                IMapper mapper,
                UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }
            public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var userToCheck = await _userRepository.GetAsync(u => u.Email == request.LoginDto.Email);
                if(userToCheck is null)
                {
                    throw new RepositoryException(Messages.UserNotFound);
                }

                if (!HashingHelper.VerifyPasswordHash(request.LoginDto.Password,
                    userToCheck.PasswordHash, userToCheck.PasswordSalt))
                {
                    throw new RepositoryException(Messages.PasswordError);
                }

                return new LoginUserDto
                {
                    Email = request.LoginDto.Email,
                    LoginIsSuccessful = true
                };
            }
        }
    }
}
