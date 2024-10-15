using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferById;

public class GetRentOfferByIdQueryHandler : IRequestHandler<GetRentOfferByIdQuery, RentOfferDTO>
{
    private readonly IRentOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetRentOfferByIdQueryHandler(IRentOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RentOfferDTO> Handle(GetRentOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new RentOfferByIdSpecification(request.Id);
        
        var rentOffer = await _repository.FirstOrDefault(specification, cancellationToken);
        if (rentOffer == null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);   
        }

        return _mapper.Map<RentOfferDTO>(rentOffer);
    }
}