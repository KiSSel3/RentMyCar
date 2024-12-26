using CarManagementService.Presentation.Models.DTOs.CarModel;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.CarModel;

public class CarModelRequestDTOValidator : AbstractValidator<CarModelRequestDTO>
{
    public CarModelRequestDTOValidator()
    {
        RuleFor(x => x.CarBrandId)
            .NotEmpty().WithMessage("Car brand ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Car model name is required.")
            .MaximumLength(100).WithMessage("Car model name must not exceed 100 characters.");
    }
}