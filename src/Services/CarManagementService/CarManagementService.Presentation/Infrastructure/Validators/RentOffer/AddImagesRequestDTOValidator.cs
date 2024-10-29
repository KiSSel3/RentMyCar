using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class AddImagesRequestDTOValidator : AbstractValidator<AddImagesRequestDTO>
{
    public AddImagesRequestDTOValidator()
    {
        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image is required.");
        
        RuleForEach(x => x.Images)
            .SetValidator(new ImageValidator());
    }
}