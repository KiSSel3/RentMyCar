using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetUserRentOffers;

public class GetUserRentOffersQueryValidator : AbstractValidator<GetUserRentOffersQuery>
{
    public GetUserRentOffersQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId must not be empty.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");
    }
}