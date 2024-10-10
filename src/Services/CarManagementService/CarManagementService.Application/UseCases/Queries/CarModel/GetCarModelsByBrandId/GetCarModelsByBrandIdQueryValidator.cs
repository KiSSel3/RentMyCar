using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByBrandId;

public class GetCarModelsByBrandIdQueryValidator : AbstractValidator<GetCarModelsByBrandIdQuery>
{
    public GetCarModelsByBrandIdQueryValidator()
    {
        RuleFor(query => query.BrandId)
            .NotEmpty().WithMessage("Brand ID is required.");
    }
}