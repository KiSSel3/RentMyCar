using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommand : IRequest
{
    public Guid RentOfferId { get; set; }
    public List<Guid> ImageIds { get; set; }
}
