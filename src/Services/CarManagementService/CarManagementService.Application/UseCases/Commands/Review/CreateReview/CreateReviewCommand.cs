using MediatR;

namespace CarManagementService.Application.UseCases.Commands.Review.CreateReview;

public class CreateReviewCommand : IRequest
{
    public Guid ReviewerId { get; set; }
    public Guid RentOfferId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}