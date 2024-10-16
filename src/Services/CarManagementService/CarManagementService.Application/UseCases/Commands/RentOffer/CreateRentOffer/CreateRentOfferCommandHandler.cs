using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.CreateRentOffer;

public class CreateRentOfferCommandHandler : IRequestHandler<CreateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public CreateRentOfferCommandHandler(
        IRentOfferRepository rentOfferRepository,
        ICarRepository carRepository,
        IMapper mapper)
    {
        _rentOfferRepository = rentOfferRepository;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateRentOfferCommand request, CancellationToken cancellationToken)
    {
        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var rentOffer = _mapper.Map<RentOfferEntity>(request);
        
        rentOffer.CreatedAt = DateTime.UtcNow;
        rentOffer.UpdatedAt = DateTime.UtcNow;
        rentOffer.IsAvailable = true;

        await _rentOfferRepository.CreateAsync(rentOffer, cancellationToken);
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