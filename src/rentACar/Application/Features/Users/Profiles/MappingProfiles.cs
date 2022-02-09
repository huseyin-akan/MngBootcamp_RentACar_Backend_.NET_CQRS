using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
        }
    }
}
