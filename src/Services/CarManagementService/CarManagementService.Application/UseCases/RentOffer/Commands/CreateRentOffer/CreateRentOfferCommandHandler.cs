using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.CreateRentOffer;

public class CreateRentOfferCommandHandler : IRequestHandler<CreateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRentOfferCommandHandler> _logger;
    
    public CreateRentOfferCommandHandler(
        IRentOfferRepository rentOfferRepository,
        ICarRepository carRepository,
        IMapper mapper,
        ILogger<CreateRentOfferCommandHandler> logger)
    {
        _rentOfferRepository = rentOfferRepository;
        _carRepository = carRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create rent offer for car with ID: {CarId}", request.CarId);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var rentOffer = _mapper.Map<RentOfferEntity>(request);
        
        rentOffer.CreatedAt = DateTime.UtcNow;
        rentOffer.UpdatedAt = DateTime.UtcNow;
        rentOffer.IsAvailable = true;
        
        await _rentOfferRepository.CreateAsync(rentOffer, cancellationToken);

        _logger.LogInformation("Successfully created rent offer with ID: {RentOfferId} for car: {CarId}", rentOffer.Id, request.CarId);
    }
    
    private async Task EnsureRelatedEntityExistsAsync(CreateRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new CarByIdSpecification(request.CarId);

        var car = await _carRepository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.CarId);
        }
    }
}