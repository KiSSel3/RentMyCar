using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;

public class UpdateRentOfferCommandHandler : IRequestHandler<UpdateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRentOfferCommandHandler> _logger;

    public UpdateRentOfferCommandHandler(
        IRentOfferRepository rentOfferRepository,
        ICarRepository carRepository,
        IMapper mapper,
        ILogger<UpdateRentOfferCommandHandler> logger)
    {
        _rentOfferRepository = rentOfferRepository;
        _carRepository = carRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to update rent offer with ID: {RentOfferId}", request.Id);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var spec = new RentOfferByIdSpecification(request.Id);
        
        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);
        }
        
        _mapper.Map(request, rentOffer);
        
        rentOffer.UpdatedAt = DateTime.UtcNow;
        
        await _rentOfferRepository.UpdateAsync(rentOffer, cancellationToken);

        _logger.LogInformation("Successfully updated rent offer with ID: {RentOfferId}", request.Id);
    }
    
    private async Task EnsureRelatedEntityExistsAsync(UpdateRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new CarByIdSpecification(request.CarId);

        var car = await _carRepository.FirstOrDefault(spec, cancellationToken);
        if (car is null)
        {
            throw new EntityNotFoundException(nameof(CarEntity), request.CarId);
        }
    }
}