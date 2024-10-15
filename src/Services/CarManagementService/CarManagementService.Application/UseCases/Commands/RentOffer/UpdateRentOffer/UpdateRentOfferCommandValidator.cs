using CarManagementService.Application.Infrastructure.CommonValidators;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.UpdateRentOffer;

public class UpdateRentOfferCommandValidator : AbstractValidator<UpdateRentOfferCommand>
{
    public UpdateRentOfferCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
        RuleFor(x => x.LocationModel)
            .SetValidator(new LocationModelValidator());
        
        RuleFor(x => x.AvailableFrom)
            .NotEmpty()
            .WithMessage("AvailableFrom is required.");
        
        RuleFor(x => x.AvailableTo)
            .NotEmpty()
            .WithMessage("AvailableTo is required.");
        
        RuleFor(x => x.AvailableTo)
            .GreaterThan(x => x.AvailableFrom)
            .WithMessage("AvailableTo must be later than AvailableFrom.");
        
        RuleFor(x => x.PricePerDay)
            .GreaterThan(0)
            .WithMessage("PricePerDay must be greater than 0.");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Description is required and must not exceed 1000 characters.");
    }
}