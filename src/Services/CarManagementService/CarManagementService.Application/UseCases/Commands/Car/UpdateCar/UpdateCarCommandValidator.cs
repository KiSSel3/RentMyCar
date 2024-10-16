using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Car.UpdateCar;

public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
{
    public UpdateCarCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("ModelId is required.");
        
        RuleFor(x => x.BodyType)
            .IsInEnum().WithMessage("Invalid body type.");
        
        RuleFor(x => x.DriveType)
            .IsInEnum().WithMessage("Invalid drive type.");
        
        RuleFor(x => x.TransmissionType)
            .IsInEnum().WithMessage("Invalid transmission type.");
        
        RuleFor(x => x.ModelYear)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Model year cannot be in the future.");
        
        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image)
                .Must(file => file.Length > 0).WithMessage("Image cannot be empty.");
        });
    }
}