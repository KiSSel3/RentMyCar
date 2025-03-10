using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class UpdateRentOfferRequestDTOValidator : AbstractValidator<UpdateRentOfferRequestDTO>
{
    public UpdateRentOfferRequestDTOValidator()
    {
        Include(new RentOfferRequestDTOValidator());
    }
}