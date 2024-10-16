using CarManagementService.Application.Exceptions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.AddImagesToRentOffer;

public class AddImagesToRentOfferCommandHandler : IRequestHandler<AddImagesToRentOffer.AddImagesToRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IImageRepository _imageRepository;

    public AddImagesToRentOfferCommandHandler(IImageRepository imageRepository, IRentOfferRepository rentOfferRepository)
    {
        _imageRepository = imageRepository;
        _rentOfferRepository = rentOfferRepository;
    }

    public async Task Handle(AddImagesToRentOffer.AddImagesToRentOfferCommand request, CancellationToken cancellationToken)
    {
        var spec = new RentOfferByIdSpecification(request.RentOfferId);

        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, cancellationToken);
        if (rentOffer is null)
        {
            throw new EntityNotFoundException(nameof(RentOfferEntity), request.RentOfferId);
        }
        
        var images = new List<ImageEntity>();
        foreach (var formFile in request.Images)
        {
            var imageByte = await ConvertToByteArrayAsync(formFile, cancellationToken);
            
            var image = new ImageEntity
            {
                RentOfferId = request.RentOfferId,
                Image = imageByte
            };
            
            images.Add(image);
        }

        await _imageRepository.AddImagesAsync(images, cancellationToken);
    }

    private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}