using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.Car.DeleteCar;

public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
{
    private readonly ICarRepository _repository;
    private readonly ILogger<DeleteCarCommandHandler> _logger;

    public DeleteCarCommandHandler(ICarRepository repository, ILogger<DeleteCarCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete car with ID: {CarId}", request.Id);

        var spec = new CarByIdSpecification(request.Id);

        var car = await _repository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.Id);
        }
        
        await _repository.DeleteAsync(car, cancellationToken);

        _logger.LogInformation("Successfully deleted car with ID: {CarId}", request.Id);
    }
}