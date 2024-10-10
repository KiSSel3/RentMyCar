using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelsByName;

public class GetCarModelsByNameQueryValidator : AbstractValidator<GetCarModelsByNameQuery>
{
    public GetCarModelsByNameQueryValidator()
    {
        RuleFor(query => query.Name)
            .NotEmpty().WithMessage("Car model name is required.")
            .MaximumLength(100).WithMessage("Car model name must not exceed 100 characters.");
    }
}