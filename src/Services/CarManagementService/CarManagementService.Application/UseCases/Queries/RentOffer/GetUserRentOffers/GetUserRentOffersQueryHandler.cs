using AutoMapper;
using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
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
        var specification = new RentOfferByUserIdSpecification(request.UserId);
        
        var totalCount = await _repository.CountAsync(specification, cancellationToken);
        
        var rentOffers = await _repository.GetAllAsync(specification, cancellationToken);
        
        var pagedList = new PagedList<RentOfferEntity>(rentOffers, totalCount, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);
        
        return _mapper.Map<PagedList<RentOfferDTO>>(pagedList);
    }
}