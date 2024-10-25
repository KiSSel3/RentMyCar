using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Queries.GetRentOfferDetails;

public class GetRentOfferDetailsQueryHandler : IRequestHandler<GetRentOfferDetailsQuery, RentOfferDetailDTO>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRentOfferDetailsQueryHandler> _logger;

    public GetRentOfferDetailsQueryHandler(
        IRentOfferRepository repository, 
        IMapper mapper,
        ILogger<GetRentOfferDetailsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RentOfferDetailDTO> Handle(GetRentOfferDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching detailed rent offer with ID: {RentOfferId}", request.Id);

        var rentOfferByIdSpec = new RentOfferByIdSpecification(request.Id);
        var rentOfferIncludeCar = new RentOfferIncludeCarSpecification();
        var rentOfferIncludeImages = new RentOfferIncludeImagesSpecification();
        
        var combinedSpec = rentOfferByIdSpec.And(rentOfferIncludeCar).And(rentOfferIncludeImages);

        var rentOffer = await _repository.FirstOrDefault(combinedSpec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);   
        }

        _logger.LogInformation("Retrieved detailed rent offer with ID: {RentOfferId}", request.Id);

        return _mapper.Map<RentOfferDetailDTO>(rentOffer);
    }
}