using Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer;
using Application.Features.IndividualCustomers.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IndividualCustomer, CreateIndividualCustomerCommand>().ReverseMap();
            //CreateMap<IndividualCustomer, UpdateIndividualCustomerCommand>().ReverseMap();
            //CreateMap<IndividualCustomer, IndividualCustomerListDto>().ReverseMap();
            CreateMap<IndividualCustomer, CreateIndividualCustomerDto>().ReverseMap();
            //CreateMap<IPaginate<IndividualCustomer>, IndividualCustomerListModel>().ReverseMap();
        }
    }
}
