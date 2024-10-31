using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;
using INotificationPublisher = CarManagementService.Application.Publishers.Interfaces.INotificationPublisher;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.DeleteRentOffer;

public class DeleteRentOfferCommandHandler : IRequestHandler<DeleteRentOfferCommand>
{
    private readonly IRentOfferRepository _repository;
    private readonly ICarRepository _carRepository;
    private readonly ILogger<DeleteRentOfferCommandHandler> _logger;
    private readonly INotificationPublisher _notificationPublisher;

    public DeleteRentOfferCommandHandler(
        IRentOfferRepository repository,
        ICarRepository carRepository,
        ILogger<DeleteRentOfferCommandHandler> logger,
        INotificationPublisher notificationPublisher)
    {
        _repository = repository;
        _carRepository = carRepository;
        _logger = logger;
        _notificationPublisher = notificationPublisher;
    }

    public async Task Handle(DeleteRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete rent offer with ID: {RentOfferId}", request.Id);

        var spec = new RentOfferByIdSpecification(request.Id);
    
        var rentOffer = await _repository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);
        }
        
        await _repository.DeleteAsync(rentOffer, cancellationToken);
        
        var car = await GetRelatedCarAsync(rentOffer.CarId, cancellationToken);
        
        await _notificationPublisher.PublishRentOfferDeletedMessageAsync(rentOffer, car, cancellationToken);

        _logger.LogInformation("Successfully deleted rent offer with ID: {RentOfferId}", request.Id);
    }
    
    private async Task<CarEntity> GetRelatedCarAsync(Guid carId, CancellationToken cancellationToken)
    {
        var carByIdSpec = new CarByIdSpecification(carId);
        var includeSpec = new CarIncludeAllSpecification();
        
        var combinedSpec = carByIdSpec.And(includeSpec);

        var car = await _carRepository.FirstOrDefault(combinedSpec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), carId);
        }

        return car;
    }
}