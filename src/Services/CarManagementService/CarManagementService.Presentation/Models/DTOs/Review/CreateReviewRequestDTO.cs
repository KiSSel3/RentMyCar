namespace CarManagementService.Presentation.Models.DTOs.Review;

public class CreateReviewRequestDTO : ReviewRequestDTO
{
    public Guid ReviewerId { get; set; }
    public Guid RentOfferId { get; set; }
}