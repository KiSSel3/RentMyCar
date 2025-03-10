using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Queries.GetUserRentOffers;

public class GetUserRentOffersQueryHandler : IRequestHandler<GetUserRentOffersQuery, PaginatedResult<RentOfferDTO>>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserRentOffersQueryHandler> _logger;

    public GetUserRentOffersQueryHandler(
        IRentOfferRepository repository, 
        IMapper mapper,
        ILogger<GetUserRentOffersQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<RentOfferDTO>> Handle(GetUserRentOffersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching rent offers for user");

        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = new RentOfferByUserIdSpecification(request.UserId) as BaseSpecification<RentOfferEntity>;
        
        if (request.IsAvailable.HasValue)
        {
            spec = spec.And(new RentOfferByAvailabilitySpecification(request.IsAvailable.Value));
        }
        
        var totalCount = await _repository.CountAsync(spec, cancellationToken);

        spec = spec.And(new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var rentOffers = await _repository.GetBySpecificationAsync(spec, cancellationToken);
        
        var rentOfferDTOs = _mapper.Map<IEnumerable<RentOfferDTO>>(rentOffers);
    
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize.Value);

        var result = new PaginatedResult<RentOfferDTO>
        {
            Collection = rentOfferDTOs,
            CurrentPage = request.PageNumber.Value,
            PageSize = request.PageSize.Value,
            TotalPageCount = totalPages,
            TotalItemCount = totalCount
        };

        _logger.LogInformation("Retrieved {TotalCount} rent offers for user: {UserId}, returning page {PageNumber} with {PageSize} items", 
            totalCount, request.UserId, request.PageNumber.Value, rentOffers.Count());

        return result;
    }
}