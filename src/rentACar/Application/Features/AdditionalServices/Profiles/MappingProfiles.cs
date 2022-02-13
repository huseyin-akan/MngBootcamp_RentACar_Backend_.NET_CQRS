using Application.Features.AdditionalServices.Commands.CreateAdditionalService;
using Application.Features.AdditionalServices.Commands.UpdateAdditionalService;
using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AdditionalService, CreateAdditionalServiceCommand>().ReverseMap();
            CreateMap<AdditionalService, UpdateAdditionalServiceCommand>().ReverseMap();
            CreateMap<AdditionalService, AdditionalServiceListDto>().ReverseMap();
            CreateMap<AdditionalService, CreateAdditionalServiceDto>().ReverseMap();
            CreateMap<AdditionalService, UpdateAdditionalServiceDto>().ReverseMap();
            CreateMap<IPaginate<AdditionalService>, AdditionalServiceListModel>().ReverseMap();
        }
    }
}
