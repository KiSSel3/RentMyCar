using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.Car.GetCars;

public class GetCarsQueryValidator : AbstractValidator<GetCarsQuery>
{
    public GetCarsQueryValidator()
    {
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

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");
        
        RuleFor(x => x)
            .Must(x => (x.PageNumber.HasValue && x.PageSize.HasValue) || (!x.PageNumber.HasValue && !x.PageSize.HasValue))
            .WithMessage("Both PageNumber and PageSize must be provided together for pagination.");
    }
}