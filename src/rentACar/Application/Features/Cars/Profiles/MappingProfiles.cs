using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.UpdateCar;
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
            CreateMap<Car, CreateCarCommand>().ReverseMap();
            CreateMap<Car, UpdateCarCommand>().ReverseMap();
            CreateMap<Car, UpdateCarAfterRentalEndCommand>().ReverseMap();
            CreateMap<Car, UpdateCarStateCommand>().ReverseMap();
            CreateMap<Car, CarListDto>().ReverseMap();
            CreateMap<Car, CreateCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();
            CreateMap<IPaginate<Car>, CarListModel>().ReverseMap();
            CreateMap<IPaginate<CarListDto>, CarListModel>().ReverseMap();
        }
    }
}
