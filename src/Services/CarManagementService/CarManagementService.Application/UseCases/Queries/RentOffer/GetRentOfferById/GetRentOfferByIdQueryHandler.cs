using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferById;

public class GetRentOfferByIdQueryHandler : IRequestHandler<GetRentOfferByIdQuery, RentOfferDTO>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRentOfferByIdQueryHandler> _logger;

    public GetRentOfferByIdQueryHandler(
        IRentOfferRepository repository, 
        IMapper mapper,
        ILogger<GetRentOfferByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RentOfferDTO> Handle(GetRentOfferByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching rent offer with ID: {RentOfferId}", request.Id);

        var specification = new RentOfferByIdSpecification(request.Id);
        
        var rentOffer = await _repository.FirstOrDefault(specification, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);   
        }

        _logger.LogInformation("Retrieved rent offer with ID: {RentOfferId}", request.Id);

        return _mapper.Map<RentOfferDTO>(rentOffer);
    }
}