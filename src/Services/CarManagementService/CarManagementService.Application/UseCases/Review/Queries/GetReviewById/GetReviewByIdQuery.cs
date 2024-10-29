using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Review.Queries.GetReviewById;

public class GetReviewByIdQuery : IRequest<ReviewDTO>
{
    public Guid Id { get; set; }
}