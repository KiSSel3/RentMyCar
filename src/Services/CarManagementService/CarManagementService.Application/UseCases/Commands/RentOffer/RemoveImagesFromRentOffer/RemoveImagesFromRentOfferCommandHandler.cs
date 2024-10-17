using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommandHandler : IRequestHandler<RemoveImagesFromRentOfferCommand>
{
    private readonly IImageRepository _imageRepository;
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ILogger<RemoveImagesFromRentOfferCommandHandler> _logger;
    
    public RemoveImagesFromRentOfferCommandHandler(
        IImageRepository imageRepository, 
        IRentOfferRepository rentOfferRepository,
        ILogger<RemoveImagesFromRentOfferCommandHandler> logger)
    {
        _imageRepository = imageRepository;
        _rentOfferRepository = rentOfferRepository;
        _logger = logger;
    }

    public async Task Handle(RemoveImagesFromRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to remove images from rent offer with ID: {RentOfferId}", request.RentOfferId);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var allImages = await _imageRepository.GetByRentOfferIdAsync(request.RentOfferId, cancellationToken);
        
        var imagesToRemove = allImages.Where(img => request.ImageIds.Contains(img.Id)).ToList();
        
        _logger.LogInformation("Found {ImageCount} images to remove for rent offer with ID: {RentOfferId}", imagesToRemove.Count, request.RentOfferId);

        if (imagesToRemove.Any())
        {
            await _imageRepository.RemoveImagesAsync(imagesToRemove, cancellationToken);
            
            _logger.LogInformation("Successfully removed {ImageCount} images from rent offer with ID: {RentOfferId}", imagesToRemove.Count, request.RentOfferId);
        }
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