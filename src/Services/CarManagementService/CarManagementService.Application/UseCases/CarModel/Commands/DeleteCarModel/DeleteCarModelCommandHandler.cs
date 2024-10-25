using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.CarModel.Commands.DeleteCarModel;

public class DeleteCarModelCommandHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly ICarModelRepository _repository;
    private readonly ILogger<DeleteCarModelCommandHandler> _logger;

    public DeleteCarModelCommandHandler(
        ICarModelRepository repository,
        ILogger<DeleteCarModelCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteCarModelCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete car model with ID: {CarModelId}", request.Id);

        var carModel = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (carModel is null)
        {
            throw new EntityNotFoundException(nameof(CarModelEntity), request.Id);
        }
        
        await _repository.DeleteAsync(carModel, cancellationToken);

        _logger.LogInformation("Successfully deleted car model with ID: {CarModelId}", request.Id);
    }
}