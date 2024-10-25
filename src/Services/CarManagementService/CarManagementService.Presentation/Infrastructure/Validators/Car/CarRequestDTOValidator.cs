using CarManagementService.Presentation.Models.DTOs.Car;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Car;

public class CarRequestDTOValidator : AbstractValidator<CarRequestDTO>
{
    public CarRequestDTOValidator()
    {
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