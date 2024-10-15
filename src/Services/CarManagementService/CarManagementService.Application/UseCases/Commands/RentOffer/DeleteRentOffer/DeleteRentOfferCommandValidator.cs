using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.DeleteRentOffer;

public class DeleteRentOfferCommandValidator : AbstractValidator<DeleteRentOfferCommand>
{
    public DeleteRentOfferCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}