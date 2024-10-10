using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetBrandByName;

public class GetBrandByNameQueryValidator : AbstractValidator<GetBrandByName.GetBrandByNameQuery>
{
    public GetBrandByNameQueryValidator()
    {
        RuleFor(query => query.Name)
            .NotEmpty().WithMessage("Brand name is required.")
            .MaximumLength(100).WithMessage("Brand name must not exceed 100 characters.");
    }
}