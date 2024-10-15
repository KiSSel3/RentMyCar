using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Repositories;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommandHandler : IRequestHandler<RemoveImagesFromRentOfferCommand>
{
    private readonly IImageRepository _imageRepository;

    public RemoveImagesFromRentOfferCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task Handle(RemoveImagesFromRentOfferCommand request, CancellationToken cancellationToken)
    {
        var allImages = await _imageRepository.GetByRentOfferIdAsync(request.RentOfferId, cancellationToken);
        
        var imagesToRemove = allImages.Where(img => request.ImageIds.Contains(img.Id)).ToList();
        if (imagesToRemove.Count == 0)
        {
            throw new EntityNotFoundException("No images found with the provided IDs for this RentOffer.");
        }
        
        await _imageRepository.RemoveImagesAsync(imagesToRemove, cancellationToken);
    }
}