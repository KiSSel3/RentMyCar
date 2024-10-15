using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.RemoveImagesFromRentOffer;

public class RemoveImagesFromRentOfferCommandValidator : AbstractValidator<RemoveImagesFromRentOfferCommand>
{
    public RemoveImagesFromRentOfferCommandValidator()
    {
        RuleFor(x => x.RentOfferId)
            .NotEmpty()
            .WithMessage("RentOfferId is required.");
        
        RuleFor(x => x.ImageIds)
            .NotEmpty()
            .WithMessage("At least one ImageId is required.");
        
        RuleForEach(x => x.ImageIds)
            .NotEmpty()
            .WithMessage("ImageId must not be empty.");
    }
}