using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.Review;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Review;

public class ReviewParametersRequestDTOValidator : AbstractValidator<ReviewParametersRequestDTO>
{
    public ReviewParametersRequestDTOValidator()
    {
        Include(new PaginationRequestDTOValidator());
        
        RuleFor(x => x.ReviewerId)
            .NotEmpty().When(x => x.ReviewerId.HasValue)
            .WithMessage("ReviewerId must not be empty when provided.");

        RuleFor(x => x.RentOfferId)
            .NotEmpty().When(x => x.RentOfferId.HasValue)
            .WithMessage("RentOfferId must not be empty when provided.");

        RuleFor(x => x.MinDate)
            .LessThanOrEqualTo(x => x.MaxDate)
            .When(x => x.MinDate.HasValue && x.MaxDate.HasValue)
            .WithMessage("MinDate must be less than or equal to MaxDate.");

        RuleFor(x => x.MaxDate)
            .GreaterThanOrEqualTo(x => x.MinDate)
            .When(x => x.MinDate.HasValue && x.MaxDate.HasValue)
            .WithMessage("MaxDate must be greater than or equal to MinDate.");

        RuleFor(x => x.MinRating)
            .InclusiveBetween(1, 5).When(x => x.MinRating.HasValue)
            .WithMessage("MinRating must be between 1 and 5.");
    }
}