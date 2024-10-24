using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;

public class GetUserRentOffersQuery : IRequest<PaginatedResult<RentOfferDTO>>
{
    public Guid UserId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}