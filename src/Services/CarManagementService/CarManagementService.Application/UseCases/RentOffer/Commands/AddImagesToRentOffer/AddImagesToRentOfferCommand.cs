using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.AddImagesToRentOffer;

public class AddImagesToRentOfferCommand : IRequest
{
    public Guid RentOfferId { get; set; }
    public List<IFormFile> Images { get; set; }
}