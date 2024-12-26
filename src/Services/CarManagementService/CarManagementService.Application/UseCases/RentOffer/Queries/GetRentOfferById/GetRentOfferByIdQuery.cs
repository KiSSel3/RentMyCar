using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Queries.GetRentOfferById;

public class GetRentOfferByIdQuery : IRequest<RentOfferDTO>
{
    public Guid Id { get; set; }
}