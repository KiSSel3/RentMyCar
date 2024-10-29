using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class CreateRentOfferRequestDTOValidator : AbstractValidator<CreateRentOfferRequestDTO>
{
    public CreateRentOfferRequestDTOValidator()
    {
        Include(new RentOfferRequestDTOValidator());
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}