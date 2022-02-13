using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            //CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            //CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap();
            //CreateMap<OperationClaim, UpdateOperationClaimDto>().ReverseMap();
            //CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();

        }
    }
}
