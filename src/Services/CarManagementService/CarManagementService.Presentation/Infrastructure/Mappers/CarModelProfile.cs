using AutoMapper;
using CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;
using CarManagementService.Application.UseCases.Commands.CarModel.UpdateCarModel;
using CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelByBrandIdAndName;
using CarManagementService.Presentation.Models.DTOs.CarModel;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        CreateMap<CarModelRequestDTO, CreateCarModelCommand>();
        CreateMap<CarModelRequestDTO, UpdateCarModelCommand>();
        
        CreateMap<CarModelParametersRequestDTO, GetCarModelByBrandIdAndNameQuery>();
    }
}