using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.RentOffer.GetRentOfferById;

public class GetRentOfferByIdQueryValidator : AbstractValidator<GetRentOfferByIdQuery>
{
    public GetRentOfferByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Id must not be empty.");
    }
}