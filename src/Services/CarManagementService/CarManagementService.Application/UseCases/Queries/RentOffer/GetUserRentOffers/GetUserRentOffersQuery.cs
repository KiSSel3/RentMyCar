using CarManagementService.Application.Helpers;
using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;

public class GetUserRentOffersQuery : IRequest<PagedList<RentOfferDTO>>
{
    public Guid UserId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}