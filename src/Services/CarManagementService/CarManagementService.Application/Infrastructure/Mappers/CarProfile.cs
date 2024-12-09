using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Car.Commands.CreateCar;
using CarManagementService.Application.UseCases.Car.Commands.UpdateCar;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, CarDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));
        
        CreateMap<CreateCarCommand, CarEntity>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
        CreateMap<UpdateCarCommand, CarEntity>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
    }
}