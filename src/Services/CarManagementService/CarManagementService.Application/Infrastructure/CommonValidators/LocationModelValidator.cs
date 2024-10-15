using CarManagementService.Domain.Data.Models;
using FluentValidation;

namespace CarManagementService.Application.Infrastructure.CommonValidators;

public class LocationModelValidator : AbstractValidator<LocationModel>
{
    public LocationModelValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("City is required and must not exceed 100 characters.");
        
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Street is required and must not exceed 100 characters.");
        
        RuleFor(x => x.Building)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Building is required and must not exceed 20 characters.");
    }
}