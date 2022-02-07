using Application.Features.Invoices.Commands.CreateInvoice;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Invoice, CreateInvoiceCommand>().ReverseMap();
            //CreateMap<Invoice, UpdateInvoiceCommand>().ReverseMap();
            //CreateMap<Invoice, InvoiceListDto>().ReverseMap();
            //CreateMap<IPaginate<Invoice>, InvoiceListModel>().ReverseMap();
        }
    }
}
