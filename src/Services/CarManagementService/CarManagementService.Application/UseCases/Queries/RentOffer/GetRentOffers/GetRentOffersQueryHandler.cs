using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.Common;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;

public class GetRentOffersQueryHandler : IRequestHandler<GetRentOffersQuery, PagedList<RentOfferDetailDTO>>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetRentOffersQueryHandler(IRentOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedList<RentOfferDetailDTO>> Handle(GetRentOffersQuery request, CancellationToken cancellationToken)
    {
        var specification = CreateSpecification(request);
        
        var totalCount = await _repository.CountAsync(specification, cancellationToken);
        
        var rentOffers = await _repository.GetAllAsync(specification, cancellationToken);
        
        var pagedList = new PagedList<RentOfferEntity>(rentOffers, totalCount, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);

        return _mapper.Map<PagedList<RentOfferDetailDTO>>(pagedList);
    }

    private ISpecification<RentOfferEntity> CreateSpecification(GetRentOffersQuery request)
    {
        BaseSpecification<RentOfferEntity> spec = new RentOfferIncludeCarSpecification();

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

        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            spec = spec.And(new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value));
        }

        return spec;
    }
}