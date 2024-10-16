using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOffers;

public class GetRentOffersQueryValidator : AbstractValidator<GetRentOffersQuery>
{
    public GetRentOffersQueryValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().When(x => x.CarId.HasValue)
            .WithMessage("CarId must not be empty when provided.");

        RuleFor(x => x.City)
            .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.City))
            .WithMessage("City name must not exceed 100 characters.");

        RuleFor(x => x.Street)
            .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Street))
            .WithMessage("Street name must not exceed 100 characters.");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
            .WithMessage("Minimum price must be non-negative.");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(x => x.MinPrice ?? 0).When(x => x.MaxPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to minimum price.");

        RuleFor(x => x.AvailableFrom)
            .LessThanOrEqualTo(x => x.AvailableTo)
            .When(x => x.AvailableFrom.HasValue && x.AvailableTo.HasValue)
            .WithMessage("Available from date must be earlier than or equal to available to date.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");
        
        RuleFor(x => x)
            .Must(x => (x.PageNumber.HasValue && x.PageSize.HasValue) || (!x.PageNumber.HasValue && !x.PageSize.HasValue))
            .WithMessage("Both PageNumber and PageSize must be provided together for pagination.");
    }
}