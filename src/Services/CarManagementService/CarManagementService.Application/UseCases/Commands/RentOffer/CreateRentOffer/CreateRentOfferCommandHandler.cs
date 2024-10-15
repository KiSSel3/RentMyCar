using AutoMapper;
using CarManagementService.Domain.Abstractions.BaseRepositories;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.CreateRentOffer;

public class CreateRentOfferCommandHandler : IRequestHandler<CreateRentOfferCommand>
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;

    public CreateRentOfferCommandHandler(IRentOfferRepository rentOfferRepository, IMapper mapper)
    {
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateRentOfferCommand request, CancellationToken cancellationToken)
    {
        var rentOffer = _mapper.Map<RentOfferEntity>(request);
        
        rentOffer.CreatedAt = DateTime.UtcNow;
        rentOffer.UpdatedAt = DateTime.UtcNow;
        rentOffer.IsAvailable = true;

        await _rentOfferRepository.CreateAsync(rentOffer, cancellationToken);
    }
    
    /*private async Task AddImagesToRentOfferAsync(Guid rentOfferId, List<IFormFile> formFiles, CancellationToken cancellationToken)
    {
        var images = new List<ImageEntity>();
        foreach (var formFile in formFiles)
        {
            var imageByte = await ConvertToByteArrayAsync(formFile, cancellationToken);
            
            var image = new ImageEntity
            {
                RentOfferId = rentOfferId,
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
    }*/
}