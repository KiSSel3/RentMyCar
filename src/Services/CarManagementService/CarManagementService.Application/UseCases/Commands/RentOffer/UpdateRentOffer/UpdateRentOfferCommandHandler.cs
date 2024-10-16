using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Car;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;

public class UpdateRentOfferCommandHandler : IRequestHandler<UpdateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public UpdateRentOfferCommandHandler(
        IRentOfferRepository rentOfferRepository,
        ICarRepository carRepository,
        IMapper mapper)
    {
        _rentOfferRepository = rentOfferRepository;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateRentOfferCommand request, CancellationToken cancellationToken)
    {
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