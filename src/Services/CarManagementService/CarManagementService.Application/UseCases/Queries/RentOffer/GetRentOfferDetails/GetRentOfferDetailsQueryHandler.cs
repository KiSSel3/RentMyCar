using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferDetails;

public class GetRentOfferDetailsQueryHandler : IRequestHandler<GetRentOfferDetailsQuery, RentOfferDetailDTO>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetRentOfferDetailsQueryHandler(IRentOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RentOfferDetailDTO> Handle(GetRentOfferDetailsQuery request, CancellationToken cancellationToken)
    {
        var rentOfferByIdSpec = new RentOfferByIdSpecification(request.Id);
        var rentOfferIncludeCar = new RentOfferIncludeCarSpecification();
        var rentOfferIncludeImages = new RentOfferIncludeImagesSpecification();
        
        var combinedSpec = rentOfferByIdSpec.And(rentOfferIncludeCar).And(rentOfferIncludeImages);

        var rentOffer = await _repository.FirstOrDefault(combinedSpec, cancellationToken);

        if (rentOffer == null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);   
        }

        return _mapper.Map<RentOfferDetailDTO>(rentOffer);
    }
}