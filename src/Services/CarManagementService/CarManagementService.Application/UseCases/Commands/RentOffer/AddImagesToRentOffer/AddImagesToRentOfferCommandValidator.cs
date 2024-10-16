using CarManagementService.Application.Infrastructure.CommonValidators;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.AddImagesToRentOffer;

public class AddImagesToRentOfferCommandValidator : AbstractValidator<AddImagesToRentOffer.AddImagesToRentOfferCommand>
{
    public AddImagesToRentOfferCommandValidator()
    {
        RuleFor(x => x.RentOfferId)
            .NotEmpty().WithMessage("RentOfferId is required.");
        
        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image is required.");
        
        RuleForEach(x => x.Images)
            .SetValidator(new ImageValidator());
    }
}