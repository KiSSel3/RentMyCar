using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.Car.CreateCar;
using CarManagementService.Application.UseCases.Commands.Car.UpdateCar;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, CarDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));
        
        CreateMap<CreateCarCommand, CarEntity>();
        CreateMap<UpdateCarCommand, CarEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}