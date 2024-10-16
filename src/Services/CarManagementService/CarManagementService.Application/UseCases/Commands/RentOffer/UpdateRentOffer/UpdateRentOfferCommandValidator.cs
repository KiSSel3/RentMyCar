using CarManagementService.Application.Infrastructure.CommonValidators;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;

public class UpdateRentOfferCommandValidator : AbstractValidator<UpdateRentOfferCommand>
{
    public UpdateRentOfferCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(x => x.LocationModel)
            .SetValidator(new LocationModelValidator());
        
        RuleFor(x => x.AvailableFrom)
            .NotEmpty().WithMessage("AvailableFrom is required.");
        
        RuleFor(x => x.AvailableTo)
            .NotEmpty().WithMessage("Available To date is required.")
            .GreaterThan(x => x.AvailableFrom)
            .WithMessage("Available To date must be later than Available From date.");
        
        RuleFor(x => x.PricePerDay)
            .GreaterThan(0).WithMessage("PricePerDay must be greater than 0.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
    }
}