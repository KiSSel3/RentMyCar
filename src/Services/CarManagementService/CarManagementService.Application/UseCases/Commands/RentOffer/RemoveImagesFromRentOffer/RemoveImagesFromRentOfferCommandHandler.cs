using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommandHandler : IRequestHandler<RemoveImagesFromRentOfferCommand>
{
    private readonly IImageRepository _imageRepository;
    private readonly IRentOfferRepository _rentOfferRepository;
    
    public RemoveImagesFromRentOfferCommandHandler(IImageRepository imageRepository, IRentOfferRepository rentOfferRepository)
    {
        _imageRepository = imageRepository;
        _rentOfferRepository = rentOfferRepository;
    }

    public async Task Handle(RemoveImagesFromRentOfferCommand request, CancellationToken cancellationToken)
    {
        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var allImages = await _imageRepository.GetByRentOfferIdAsync(request.RentOfferId, cancellationToken);
        
        var imagesToRemove = allImages.Where(img => request.ImageIds.Contains(img.Id));
        if (!imagesToRemove.Any())
        {
            throw new EntityNotFoundException("No images found with the provided IDs for this RentOffer.");
        }
        
        await _imageRepository.RemoveImagesAsync(imagesToRemove, cancellationToken);
    }
    
    private async Task EnsureRelatedEntityExistsAsync(RemoveImagesFromRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.RentOfferId);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.RentOfferId);
        }
    }
}