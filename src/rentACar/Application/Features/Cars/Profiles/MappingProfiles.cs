using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Cars.Dtos;
using Application.Features.Cars.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Profiles
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Car, CreateBrandCommand>().ReverseMap();
            //CreateMap<Brand, UpdateBrandCommand>().ReverseMap();
            CreateMap<Car, CarListDto>().ReverseMap();
            CreateMap<IPaginate<Car>, CarListModel>().ReverseMap();
        }
    }
}
