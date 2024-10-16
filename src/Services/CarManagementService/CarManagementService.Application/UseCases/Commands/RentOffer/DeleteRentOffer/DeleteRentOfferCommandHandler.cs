using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.DeleteRentOffer;

public class DeleteRentOfferCommandHandler : IRequestHandler<DeleteRentOfferCommand>
{
    private readonly IRentOfferRepository _repository;

    public DeleteRentOfferCommandHandler(IRentOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.Id);

        var rentOffer = await _repository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);
        }

        await _repository.DeleteAsync(rentOffer, cancellationToken);
    }
}