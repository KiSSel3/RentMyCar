using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using CarManagementService.Domain.Specifications.Review;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;

public class GetUserRentOffersQueryHandler : IRequestHandler<GetUserRentOffersQuery, PagedList<RentOfferDTO>>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetUserRentOffersQueryHandler(IRentOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedList<RentOfferDTO>> Handle(GetUserRentOffersQuery request, CancellationToken cancellationToken)
    {
        request.PageSize ??= int.MaxValue;
        request.PageNumber ??= 1;
        
        var rentOfferByUserIdSpec = new RentOfferByUserIdSpecification(request.UserId);
        var rentOfferPaginationSpec = new RentOfferPaginationSpecification(request.PageNumber.Value, request.PageSize.Value);

        var combinedSpec = rentOfferPaginationSpec.And(rentOfferByUserIdSpec);
        
        var totalCount = await _repository.CountAsync(rentOfferByUserIdSpec, cancellationToken);
        
        var rentOffers = await _repository.GetBySpecificationAsync(combinedSpec, cancellationToken);
        
        var pagedList = new PagedList<RentOfferEntity>(rentOffers, totalCount, request.PageNumber.Value, request.PageSize.Value);
        
        return _mapper.Map<PagedList<RentOfferDTO>>(pagedList);
    }
}