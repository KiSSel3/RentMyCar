using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.DeleteRentOffer;

public class DeleteRentOfferCommand : IRequest
{
    public Guid Id { get; set; }
}