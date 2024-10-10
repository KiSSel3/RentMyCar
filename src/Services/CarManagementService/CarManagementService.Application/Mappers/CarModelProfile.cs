using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;
using CarManagementService.Application.UseCases.Commands.CarModel.UpdateCarModel;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Application.Mappers;

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