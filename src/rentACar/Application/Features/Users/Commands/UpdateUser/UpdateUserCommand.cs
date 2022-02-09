using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserDto>
    {
        public UserRegistrationUpdateDto RegisterUpdateDto { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules userBusinessRules;

            public UpdateUserCommandHandler(IUserRepository userRepository,
                IMapper mapper,
                UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {

                var userToUpdate= _mapper.Map<User>(request);
                var updatedUser = await _userRepository.UpdateAsync(userToUpdate);
                var userToReturn = _mapper.Map<UpdateUserDto>(updatedUser);
                return userToReturn;
            }
        }
    }
}
