using CarManagementService.Presentation.Models.DTOs.Review;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Review;

public class CreateReviewRequestDTOValidator : AbstractValidator<CreateReviewRequestDTO>
{
    public CreateReviewRequestDTOValidator()
    {
        Include(new ReviewRequestDTOValidator());
        
        RuleFor(x => x.ReviewerId)
            .NotEmpty().WithMessage("Reviewer ID is required.");
        
        RuleFor(x => x.RentOfferId)
            .NotEmpty().WithMessage("Rent offer ID is required.");
    }
}