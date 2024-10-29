using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class RentOfferRequestDTOValidator : AbstractValidator<RentOfferRequestDTO>
{
    public RentOfferRequestDTOValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("CarId is required.");
        
        RuleFor(x => x.LocationModel)
            .SetValidator(new LocationModelValidator());
        
        RuleFor(x => x.PricePerDay)
            .GreaterThan(0).WithMessage("PricePerDay must be greater than 0.");
        
        RuleFor(x => x.AvailableFrom)
            .NotEmpty().WithMessage("AvailableFrom is required.");
        
        RuleFor(x => x.AvailableTo)
            .NotEmpty().WithMessage("AvailableTo is required.");
        
        RuleFor(x => x.AvailableTo)
            .GreaterThan(x => x.AvailableFrom)
            .WithMessage("AvailableTo must be later than AvailableFrom.");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Description is required and must not exceed 1000 characters.");
    }
}