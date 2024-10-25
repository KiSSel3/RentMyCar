using MediatR;

namespace CarManagementService.Application.UseCases.Review.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public Guid Id { get; set; }
}