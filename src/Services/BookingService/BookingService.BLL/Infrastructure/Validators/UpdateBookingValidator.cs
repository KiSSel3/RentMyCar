using BookingService.BLL.Models.DTOs.Booking;
using FluentValidation;

namespace BookingService.BLL.Infrastructure.Validators;

public class UpdateBookingValidator : AbstractValidator<UpdateBookingDTO>
{
    public UpdateBookingValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Booking status is required")
            .IsInEnum().WithMessage("Invalid booking status");

        RuleFor(x => x.Message)
            .MaximumLength(1000).When(x => x.Message is not null)
            .WithMessage("Message cannot exceed 1000 characters");
    }
}