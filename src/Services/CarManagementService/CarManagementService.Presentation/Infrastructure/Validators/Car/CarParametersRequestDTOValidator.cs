using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.Car;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Car;

public class CarParametersRequestDTOValidator : AbstractValidator<CarParametersRequestDTO>
{
    public CarParametersRequestDTOValidator()
    {
        Include(new PaginationRequestDTOValidator());
        
        RuleFor(x => x.ModelId)
            .NotEqual(Guid.Empty).When(x => x.ModelId.HasValue)
            .WithMessage("ModelId must be a valid GUID.");

        RuleFor(x => x.BodyType)
            .IsInEnum().When(x => x.BodyType.HasValue)
            .WithMessage("Invalid body type specified.");

        RuleFor(x => x.DriveType)
            .IsInEnum().When(x => x.DriveType.HasValue)
            .WithMessage("Invalid drive type specified.");

        RuleFor(x => x.TransmissionType)
            .IsInEnum().When(x => x.TransmissionType.HasValue)
            .WithMessage("Invalid transmission type specified.");

        RuleFor(x => x.Year)
            .Must(year => year.Value.Year >= 1900 && year.Value.Year <= DateTime.Now.Year)
            .When(x=> x.Year.HasValue)
            .WithMessage("Year must be between 1900 and the current year.");

        RuleFor(x => x.MinYear)
            .Must(year => year.Value.Year >= 1900 && year.Value.Year <= DateTime.Now.Year)
            .When(x=> x.Year.HasValue)
            .WithMessage("MinYear must be between 1900 and the current year.");

        RuleFor(x => x.MaxYear)
            .Must(year => year.Value.Year >= 1900 && year.Value.Year <= DateTime.Now.Year)
            .When(x=> x.Year.HasValue)
            .WithMessage("MaxYear must be between 1900 and the current year.");
    }
}