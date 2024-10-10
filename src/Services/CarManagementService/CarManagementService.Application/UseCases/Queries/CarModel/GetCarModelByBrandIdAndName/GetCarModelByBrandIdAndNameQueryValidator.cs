using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelByBrandIdAndName;

public class GetCarModelByBrandIdAndNameQueryValidator : AbstractValidator<GetCarModelByBrandIdAndNameQuery>
{
    public GetCarModelByBrandIdAndNameQueryValidator()
    {
        RuleFor(query => query.BrandId)
            .NotEmpty().WithMessage("Brand ID is required.");

        RuleFor(query => query.Name)
            .NotEmpty().WithMessage("Car model name is required.")
            .MaximumLength(100).WithMessage("Car model name must not exceed 100 characters.");
    }
}