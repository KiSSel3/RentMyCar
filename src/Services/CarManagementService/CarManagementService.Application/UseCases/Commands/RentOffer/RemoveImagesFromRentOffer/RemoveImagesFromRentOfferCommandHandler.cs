using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommandHandler : IRequestHandler<RemoveImagesFromRentOfferCommand>
{
    private readonly IImageRepository _repository;

    public RemoveImagesFromRentOfferCommandHandler(IImageRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveImagesFromRentOfferCommand request, CancellationToken cancellationToken)
    {
        var allImages = await _repository.GetByRentOfferIdAsync(request.RentOfferId, cancellationToken);
        
        var imagesToRemove = allImages.Where(img => request.ImageIds.Contains(img.Id));
        if (!imagesToRemove.Any())
        {
            throw new EntityNotFoundException("No images found with the provided IDs for this RentOffer.");
        }
        
        await _repository.RemoveImagesAsync(imagesToRemove, cancellationToken);
    }
}