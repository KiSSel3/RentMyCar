using CarManagementService.Application.Models.DTOs;
using MediatR;

namespace CarManagementService.Application.UseCases.Review.Commands.CreateReview;

public class CreateReviewCommand : IRequest<ReviewDTO>
{
    public Guid ReviewerId { get; set; }
    public Guid RentOfferId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}