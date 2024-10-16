using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.UpdateReview;

public class UpdateReviewCommand : IRequest
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}