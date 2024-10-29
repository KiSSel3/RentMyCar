using AutoMapper;
using CarManagementService.Application.UseCases.Car.Commands.CreateCar;
using CarManagementService.Application.UseCases.Car.Commands.UpdateCar;
using CarManagementService.Application.UseCases.Car.Queries.GetCars;
using CarManagementService.Presentation.Models.DTOs.Car;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarRequestDTO, CreateCarCommand>();
        CreateMap<CarRequestDTO, UpdateCarCommand>();
        
        CreateMap<CarParametersRequestDTO, GetCarsQuery>();
    }
}