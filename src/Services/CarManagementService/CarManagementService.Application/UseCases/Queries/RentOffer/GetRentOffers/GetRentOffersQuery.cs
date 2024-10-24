using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;

public class GetRentOffersQuery : IRequest<PaginatedResult<RentOfferDetailDTO>>
{
    public Guid? CarId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}