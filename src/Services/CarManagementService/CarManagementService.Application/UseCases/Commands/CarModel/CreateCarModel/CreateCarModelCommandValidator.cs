using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.CarModel.CreateCarModel;

public class CreateCarModelCommandValidator : AbstractValidator<CreateCarModelCommand>
{
    public CreateCarModelCommandValidator()
    {
        RuleFor(command => command.CarBrandId)
            .NotEmpty().WithMessage("Car brand ID is required.");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Car model name is required.")
            .MaximumLength(100).WithMessage("Car model name must not exceed 100 characters.");
    }
}