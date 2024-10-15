using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferById;

public class GetRentOfferByIdQuery : IRequest<RentOfferDTO>
{
    public Guid Id { get; set; }
}