using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.Extensions.Logging;
using INotificationPublisher = CarManagementService.Application.Publishers.Interfaces.INotificationPublisher;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.CreateRentOffer;

public class CreateRentOfferCommandHandler : IRequestHandler<CreateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRentOfferCommandHandler> _logger;
    private readonly INotificationPublisher _notificationPublisher;
    
    public CreateRentOfferCommandHandler(
        IRentOfferRepository rentOfferRepository,
        ICarRepository carRepository,
        IMapper mapper,
        ILogger<CreateRentOfferCommandHandler> logger,
        INotificationPublisher notificationPublisher)
    {
        _rentOfferRepository = rentOfferRepository;
        _carRepository = carRepository;
        _mapper = mapper;
        _logger = logger;
        _notificationPublisher = notificationPublisher;
    }

    public async Task Handle(CreateRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create rent offer for car with ID: {CarId}", request.CarId);

        var car = await GetRelatedCarEntityAsync(request.CarId, cancellationToken);
        
        var rentOffer = _mapper.Map<RentOfferEntity>(request);
        
        rentOffer.CreatedAt = DateTime.UtcNow;
        rentOffer.UpdatedAt = DateTime.UtcNow;
        rentOffer.IsAvailable = true;
        
        await _rentOfferRepository.CreateAsync(rentOffer, cancellationToken);

        _logger.LogInformation("Successfully created rent offer with ID: {RentOfferId} for car: {CarId}", rentOffer.Id, request.CarId);

        await _notificationPublisher.PublishRentOfferCreatedMessageAsync(rentOffer, car, cancellationToken);
    }
    
    private async Task<CarEntity> GetRelatedCarEntityAsync(Guid carId, CancellationToken cancellationToken)
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