using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Jwt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAccessToken
{
    public class GetAccessTokenQuery : IRequest<AccessToken>
    {
        User user;
        public class GetAccessTokenQueryHandler : IRequestHandler<GetAccessTokenQuery, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public GetAccessTokenQueryHandler(IUserRepository userRepository,
                IMapper mapper,
                ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;                
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public Task<AccessToken> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
            {
                var claims = this._userRepository.GetClaims(request.user);
                var accessToken = this._tokenHelper.CreateToken(request.user, claims);
                return accessToken;
            }
        }

    }

}
