using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class RentOfferParametersRequestDTOValidator : AbstractValidator<RentOfferParametersRequestDTO>
{
    public RentOfferParametersRequestDTOValidator()
    {
        Include(new PaginationRequestDTOValidator());
        
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
    }
}