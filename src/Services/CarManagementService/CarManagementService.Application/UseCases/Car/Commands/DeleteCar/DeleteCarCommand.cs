using MediatR;

namespace CarManagementService.Application.UseCases.Car.Commands.DeleteCar;

public class DeleteCarCommand : IRequest
{
    public Guid Id { get; set; }
}