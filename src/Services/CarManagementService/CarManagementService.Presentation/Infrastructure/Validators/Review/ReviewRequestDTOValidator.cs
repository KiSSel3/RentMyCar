using CarManagementService.Presentation.Models.DTOs.Review;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Review;

public class ReviewRequestDTOValidator : AbstractValidator<ReviewRequestDTO>
{
    public ReviewRequestDTOValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        
        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters.");
    }
}