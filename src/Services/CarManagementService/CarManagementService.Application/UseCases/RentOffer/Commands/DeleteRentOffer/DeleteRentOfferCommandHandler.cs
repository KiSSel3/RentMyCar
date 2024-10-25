using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.DeleteRentOffer;

public class DeleteRentOfferCommandHandler : IRequestHandler<DeleteRentOfferCommand>
{
    private readonly IRentOfferRepository _repository;
    private readonly ILogger<DeleteRentOfferCommandHandler> _logger;

    public DeleteRentOfferCommandHandler(
        IRentOfferRepository repository,
        ILogger<DeleteRentOfferCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete rent offer with ID: {RentOfferId}", request.Id);

        var spec = new RentOfferByIdSpecification(request.Id);
        
        var rentOffer = await _repository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.Id);
        }
        
        await _repository.DeleteAsync(rentOffer, cancellationToken);

        _logger.LogInformation("Successfully deleted rent offer with ID: {RentOfferId}", request.Id);
    }
}