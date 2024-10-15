using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferDetails;

public class GetRentOfferDetailsQueryValidator : AbstractValidator<GetRentOfferDetailsQuery>
{
    public GetRentOfferDetailsQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Id must not be empty.");
    }
}