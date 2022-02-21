using Application.Features.PromotionCodes.Commands.CreatePromotionCode;
using Application.Features.PromotionCodes.Commands.UpdatePromotionCode;
using Application.Features.PromotionCodes.Dtos;
using Application.Features.PromotionCodes.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PromotionCode, CreatePromotionCodeCommand>().ReverseMap();
            CreateMap<PromotionCode, UpdatePromotionCodeCommand>().ReverseMap();
            CreateMap<PromotionCode, PromotionCodeListDto>().ReverseMap();
            CreateMap<PromotionCode, CreatedPromotionCodeDto>().ReverseMap();
            CreateMap<PromotionCode, UpdatedPromotionCodeDto>().ReverseMap();
            CreateMap<IPaginate<PromotionCode>, PromotionCodeListModel>().ReverseMap();
        }
    }
}
