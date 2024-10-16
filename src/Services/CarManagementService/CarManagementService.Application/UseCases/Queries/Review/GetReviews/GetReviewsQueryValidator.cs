using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.Review.GetReviews;

public class GetReviewsQueryValidator : AbstractValidator<GetReviewsQuery>
{
    public GetReviewsQueryValidator()
    {
        RuleFor(x => x.ReviewerId)
            .NotEmpty().When(x => x.ReviewerId.HasValue)
            .WithMessage("ReviewerId must not be empty when provided.");

        RuleFor(x => x.RentOfferId)
            .NotEmpty().When(x => x.RentOfferId.HasValue)
            .WithMessage("RentOfferId must not be empty when provided.");

        RuleFor(x => x.MinDate)
            .LessThanOrEqualTo(x => x.MaxDate).When(x => x.MinDate.HasValue && x.MaxDate.HasValue)
            .WithMessage("MinDate must be less than or equal to MaxDate.");

        RuleFor(x => x.MaxDate)
            .GreaterThanOrEqualTo(x => x.MinDate).When(x => x.MinDate.HasValue && x.MaxDate.HasValue)
            .WithMessage("MaxDate must be greater than or equal to MinDate.");

        RuleFor(x => x.MinRating)
            .InclusiveBetween(1, 5).When(x => x.MinRating.HasValue)
            .WithMessage("MinRating must be between 1 and 5.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");
    }
}