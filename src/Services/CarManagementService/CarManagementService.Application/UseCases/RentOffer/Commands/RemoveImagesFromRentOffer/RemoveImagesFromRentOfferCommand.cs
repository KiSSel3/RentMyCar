using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommand : IRequest
{
    public Guid RentOfferId { get; set; }
    public List<Guid> ImageIds { get; set; }
}
