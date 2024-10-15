using MediatR;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.DeleteRentOffer;

public class DeleteRentOfferCommand : IRequest
{
    public Guid Id { get; set; }
}