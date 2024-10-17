using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;

public class GetRentOffersQueryHandler : IRequestHandler<GetRentOffersQuery, PagedList<RentOfferDetailDTO>>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRentOffersQueryHandler> _logger;

    public GetRentOffersQueryHandler(
        IRentOfferRepository repository, 
        IMapper mapper,
        ILogger<GetRentOffersQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedList<RentOfferDetailDTO>> Handle(GetRentOffersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching rent offers with parameters");

        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);
        
        var totalCount = await _repository.CountAsync(spec, cancellationToken);
        
        spec = spec.And(new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var rentOffers = await _repository.GetBySpecificationAsync(spec, cancellationToken);
        
        var pagedList = new PagedList<RentOfferEntity>(rentOffers, totalCount, request.PageNumber.Value, request.PageSize.Value);

        _logger.LogInformation("Retrieved {TotalCount} rent offers, returning page {PageNumber} with {PageSize} items", 
            totalCount, request.PageNumber.Value, rentOffers.Count());

        return _mapper.Map<PagedList<RentOfferDetailDTO>>(pagedList);
    }

    private BaseSpecification<RentOfferEntity> CreateSpecification(GetRentOffersQuery request)
    {
        var spec = new RentOfferIncludeCarSpecification() as BaseSpecification<RentOfferEntity>;

        spec = spec.And(new RentOfferIncludeImagesSpecification());
        
        if (request.CarId.HasValue)
        {
            spec = spec.And(new RentOfferByCarIdSpecification(request.CarId.Value));
        }

        if (!string.IsNullOrEmpty(request.City))
        {
            spec = spec.And(new RentOfferByCitySpecification(request.City));
        }

        if (!string.IsNullOrEmpty(request.Street))
        {
            spec = spec.And(new RentOfferByStreetSpecification(request.Street));
        }
        
        if (request.MinPrice.HasValue)
        {
            spec = spec.And(new RentOfferByMinPriceSpecification(request.MinPrice.Value));
        }

        if (request.MaxPrice.HasValue)
        {
            spec = spec.And(new RentOfferByMaxPriceSpecification(request.MaxPrice.Value));
        }

        if (request.AvailableFrom.HasValue)
        {
            spec = spec.And(new RentOfferByMinAvailableDateSpecification(request.AvailableFrom.Value));
        }

        if (request.AvailableTo.HasValue)
        {
            spec = spec.And(new RentOfferByMaxAvailableDateSpecification(request.AvailableTo.Value));
        }

        return spec;
    }
}