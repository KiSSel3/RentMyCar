using CarManagementService.Presentation.Infrastructure.Validators.Common;
using CarManagementService.Presentation.Models.DTOs.Common;
using CarManagementService.Presentation.Models.DTOs.RentOffer;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.RentOffer;

public class UserRentOffersRequestDTOValidator : AbstractValidator<UserRentOffersRequestDTO>
{
    public UserRentOffersRequestDTOValidator()
    {
        Include(new PaginationRequestDTOValidator());
    }
}