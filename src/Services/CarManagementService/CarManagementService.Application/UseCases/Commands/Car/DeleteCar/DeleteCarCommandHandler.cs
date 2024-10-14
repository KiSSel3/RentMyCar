using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Car.DeleteCar;

public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
{
    private readonly ICarRepository _repository;

    public DeleteCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var spec = new CarByIdSpecification(request.Id);

        var car = await _repository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.Id);
        }

        await _repository.DeleteAsync(car, cancellationToken);
    }
}