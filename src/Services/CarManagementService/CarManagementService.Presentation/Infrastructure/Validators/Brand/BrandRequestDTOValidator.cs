using CarManagementService.Presentation.Models.DTOs.Brand;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Brand;

public class BrandRequestDTOValidator : AbstractValidator<BrandRequestDTO>
{
    public BrandRequestDTOValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Brand name is required.")
            .MaximumLength(100).WithMessage("Brand name must not exceed 100 characters.");
    }
}