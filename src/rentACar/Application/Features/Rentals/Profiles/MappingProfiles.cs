﻿using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using Application.Features.Rentals.Commands.EndRentalForCC;
using Application.Features.Rentals.Commands.EndRentalForIC;
using Application.Features.Rentals.Commands.RentForCorporateCustomer;
using Application.Features.Rentals.Commands.RentForIndividualCustomer;
using Application.Features.Rentals.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Profiles
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Rental, RentForIndividualCustomerCommand>().ReverseMap();
            CreateMap<Rental, RentForCorporateCustomerCommand>().ReverseMap();
            CreateMap<Rental, EndRentalForCCCommand>().ReverseMap();
            CreateMap<Rental, EndRentalForICCommand>().ReverseMap();
            //CreateMap<Rental, UpdateRentalCommand>().ReverseMap();
            //CreateMap<Rental, RentalListDto>().ReverseMap();
            //CreateMap<IPaginate<Rental>, RentalListModel>().ReverseMap();
        }
    }
}
