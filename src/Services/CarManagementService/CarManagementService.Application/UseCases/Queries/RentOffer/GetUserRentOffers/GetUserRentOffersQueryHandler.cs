using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using CarManagementService.Domain.Specifications.Review;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;

public class GetUserRentOffersQueryHandler : IRequestHandler<GetUserRentOffersQuery, PagedList<RentOfferDTO>>
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

    public async Task<PagedList<RentOfferDTO>> Handle(GetUserRentOffersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching rent offers for user");

        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var rentOfferByUserIdSpec = new RentOfferByUserIdSpecification(request.UserId);
        var rentOfferPaginationSpec = new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value);

        var combinedSpec = rentOfferPaginationSpec.And(rentOfferByUserIdSpec);
        
        var totalCount = await _repository.CountAsync(rentOfferByUserIdSpec, cancellationToken);
        
        var rentOffers = await _repository.GetBySpecificationAsync(combinedSpec, cancellationToken);
        
        var pagedList = new PagedList<RentOfferEntity>(rentOffers, totalCount, request.PageNumber.Value, request.PageSize.Value);

        _logger.LogInformation("Retrieved {TotalCount} rent offers for user: {UserId}, returning page {PageNumber} with {PageSize} items", 
            totalCount, request.UserId, request.PageNumber.Value, rentOffers.Count());
        
        return _mapper.Map<PagedList<RentOfferDTO>>(pagedList);
    }
}