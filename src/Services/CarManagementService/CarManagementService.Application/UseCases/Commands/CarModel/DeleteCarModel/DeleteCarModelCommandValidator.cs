using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.CarModel.DeleteCarModel;

public class DeleteCarModelCommandValidator : AbstractValidator<DeleteCarModelCommand>
{
    public DeleteCarModelCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Car model ID is required.");
    }
}