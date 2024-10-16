using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public Guid Id { get; set; }
}