using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Queries.GetRentOfferDetails;

public class GetRentOfferDetailsQuery : IRequest<RentOfferDetailDTO>
{
    public Guid Id { get; set; }
}