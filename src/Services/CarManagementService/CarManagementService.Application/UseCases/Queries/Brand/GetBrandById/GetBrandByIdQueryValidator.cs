using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.Brand.GetBrandById;

public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Brand ID is required.");
    }
}