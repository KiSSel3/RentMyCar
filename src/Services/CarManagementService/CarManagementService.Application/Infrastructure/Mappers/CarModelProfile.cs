using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.CarModel.Commands.CreateCarModel;
using CarManagementService.Application.UseCases.CarModel.Commands.UpdateCarModel;
using CarManagementService.Domain.Data.Entities;

namespace CarManagementService.Application.Infrastructure.Mappers;

public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        CreateMap<CarModelEntity, CarModelDTO>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));
        
        CreateMap<CreateCarModelCommand, CarModelEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCarModelCommand, CarModelEntity>();
    }
}