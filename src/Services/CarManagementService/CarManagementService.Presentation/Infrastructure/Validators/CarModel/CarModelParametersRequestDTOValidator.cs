using CarManagementService.Presentation.Models.DTOs.CarModel;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.CarModel;

public class CarModelParametersRequestDTOValidator : AbstractValidator<CarModelParametersRequestDTO>
{
    public CarModelParametersRequestDTOValidator()
    {
        RuleFor(query => query.BrandId)
            .NotEmpty().WithMessage("Brand ID is required.");

        RuleFor(query => query.Name)
            .NotEmpty().WithMessage("Car model name is required.")
            .MaximumLength(100).WithMessage("Car model name must not exceed 100 characters.");
    }
}