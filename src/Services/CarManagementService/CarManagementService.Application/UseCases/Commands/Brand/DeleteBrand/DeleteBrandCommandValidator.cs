using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Brand.DeleteBrand;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Brand ID is required.");
    }
}