using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Car.Queries.GetCarById;

public class GetCarByIdQuery : IRequest<CarDTO>
{
    public Guid CarId { get; set; }
}