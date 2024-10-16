using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviewById;

public class GetReviewByIdQuery : IRequest<ReviewDTO>
{
    public Guid Id { get; set; }
}