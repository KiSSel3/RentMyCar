using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Queries.GetRentOffers;

public class GetRentOffersQueryHandler : IRequestHandler<GetRentOffersQuery, PaginatedResult<RentOfferDetailDTO>>
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

    public async Task<PaginatedResult<RentOfferDetailDTO>> Handle(GetRentOffersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching rent offers with parameters");

        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var spec = CreateSpecification(request);
        
        var totalCount = await _repository.CountAsync(spec, cancellationToken);
        
        spec = spec.And(new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        
        var rentOffers = await _repository.GetBySpecificationAsync(spec, cancellationToken);
        
        var rentOfferDTOs = _mapper.Map<IEnumerable<RentOfferDetailDTO>>(rentOffers);
    
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize.Value);

        var result = new PaginatedResult<RentOfferDetailDTO>
        {
            Collection = rentOfferDTOs,
            CurrentPage = request.PageNumber.Value,
            PageSize = request.PageSize.Value,
            TotalPageCount = totalPages,
            TotalItemCount = totalCount
        };
        
        _logger.LogInformation("Retrieved {TotalCount} rent offers, returning page {PageNumber} with {PageSize} items", 
            totalCount, request.PageNumber.Value, rentOffers.Count());

        return result;
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