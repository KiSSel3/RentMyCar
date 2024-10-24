using AutoMapper;
using CarManagementService.Application.UseCases.Commands.Car.CreateCar;
using CarManagementService.Application.UseCases.Commands.Car.UpdateCar;
using CarManagementService.Application.UseCases.Queries.Car.GetCars;
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