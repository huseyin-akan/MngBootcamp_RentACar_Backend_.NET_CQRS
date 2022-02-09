using Application.Features.Users.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUser
{
    public class GetUserListDtoQuery :IRequest<UserListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetUserListDtoQueryHandler : IRequestHandler<GetUserListDtoQuery, UserListModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUserListDtoQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetUserListDtoQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetListAsync(
                    size: request.PageRequest.PageSize,
                    index: request.PageRequest.Page);
                var userListModel = _mapper.Map<UserListModel>(users);
                return userListModel;
            }
        }
    }
}
