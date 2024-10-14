using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Car.DeleteCar;

public class DeleteCarCommand : IRequest
{
    public Guid Id { get; set; }
}