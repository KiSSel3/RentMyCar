using AutoMapper;
using CarManagementService.Application.UseCases.CarModel.Commands.CreateCarModel;
using CarManagementService.Application.UseCases.CarModel.Commands.UpdateCarModel;
using CarManagementService.Application.UseCases.CarModel.Queries.GetCarModelByBrandIdAndName;
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