using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class RemoveImagesRequestDTOValidator : AbstractValidator<RemoveImagesRequestDTO>
{
    public RemoveImagesRequestDTOValidator()
    {
        RuleFor(x => x.ImageIds)
            .NotEmpty().WithMessage("At least one ImageId is required.");
        
        RuleForEach(x => x.ImageIds)
            .NotEmpty().WithMessage("ImageId must not be empty.");
    }
}