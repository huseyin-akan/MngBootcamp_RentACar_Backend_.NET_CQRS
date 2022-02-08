using Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer;
using Application.Features.CorporateCustomers.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CorporateCustomer, CreateCorporateCustomerCommand>().ReverseMap();
            CreateMap<CorporateCustomer, CreateCorporateCustomerDto>().ReverseMap();
            //CreateMap<CorporateCustomer, UpdateCorporateCustomerCommand>().ReverseMap();
            //CreateMap<CorporateCustomer, CorporateCustomerListDto>().ReverseMap();
            //CreateMap<IPaginate<CorporateCustomer>, CorporateCustomerListModel>().ReverseMap();
        }
    }
}
