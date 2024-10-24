using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviews;

public class GetReviewsQuery : IRequest<PaginatedResult<ReviewDTO>>
{
    public Guid? ReviewerId { get; set; }
    public Guid? RentOfferId { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public int? MinRating { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}