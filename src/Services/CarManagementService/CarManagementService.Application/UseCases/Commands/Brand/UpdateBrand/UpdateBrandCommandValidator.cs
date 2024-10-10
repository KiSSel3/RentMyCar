using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Brand ID is required.");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Brand name is required.")
            .MaximumLength(100).WithMessage("Brand name must not exceed 100 characters.");
    }
}