using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.AddImagesToRentOffer;

public class AddImagesToRentOfferCommandHandler : IRequestHandler<AddImagesToRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<AddImagesToRentOfferCommandHandler> _logger;

    public AddImagesToRentOfferCommandHandler(
        IImageRepository imageRepository, 
        IRentOfferRepository rentOfferRepository,
        ILogger<AddImagesToRentOfferCommandHandler> logger)
    {
        _imageRepository = imageRepository;
        _rentOfferRepository = rentOfferRepository;
        _logger = logger;
    }

    public async Task Handle(AddImagesToRentOfferCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to add images to rent offer with ID: {RentOfferId}", request.RentOfferId);

        await EnsureRelatedEntityExistsAsync(request, cancellationToken);
        
        var images = new List<ImageEntity>();
        foreach (var formFile in request.Images)
        {
            _logger.LogInformation("Processing image: {FileName} for rent offer: {RentOfferId}", formFile.FileName, request.RentOfferId);
            
            var imageByte = await ConvertToByteArrayAsync(formFile, cancellationToken);
            
            var image = new ImageEntity
            {
                RentOfferId = request.RentOfferId,
                Image = imageByte
            };
            
            images.Add(image);
        }
        
        await _imageRepository.AddImagesAsync(images, cancellationToken);

        _logger.LogInformation("Successfully added {ImageCount} images to rent offer: {RentOfferId}", images.Count, request.RentOfferId);
    }

    private async Task EnsureRelatedEntityExistsAsync(AddImagesToRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.RentOfferId);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.RentOfferId);
        }
    }
    
    private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}