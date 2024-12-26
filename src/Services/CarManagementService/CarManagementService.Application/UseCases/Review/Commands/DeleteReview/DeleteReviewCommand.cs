using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Review.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest<ReviewDTO>
{
    public Guid Id { get; set; }
}