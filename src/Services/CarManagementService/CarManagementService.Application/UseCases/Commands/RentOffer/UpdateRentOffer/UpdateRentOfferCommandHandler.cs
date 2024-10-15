using AutoMapper;
using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;

public class UpdateRentOfferCommandHandler : IRequestHandler<UpdateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;

    public UpdateRentOfferCommandHandler(IRentOfferRepository rentOfferRepository, IMapper mapper)
    {
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.Id);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);
        }

        _mapper.Map(request, rentOffer);
        
        rentOffer.UpdatedAt = DateTime.UtcNow;

        await _rentOfferRepository.UpdateAsync(rentOffer, cancellationToken);
    }
}