using BookingService.BLL.Models.DTOs.Booking;
using FluentValidation;

namespace BookingService.BLL.Infrastructure.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingDTO>  
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.RentOfferId)
            .NotEmpty().WithMessage("Rent offer ID is required");

        RuleFor(x => x.RentalStart)
            .NotEmpty()
            .WithMessage("Rental start date is required")
            .Must(x => x.Date >= DateTime.UtcNow.Date)
            .WithMessage("Rental start date cannot be in the past");

        RuleFor(x => x.RentalEnd)
            .NotEmpty()
            .WithMessage("Rental end date is required")
            .GreaterThanOrEqualTo(x => x.RentalStart)
            .WithMessage("Rental end date must be greater than or equal to start date");

        RuleFor(x => x.Message)
            .MaximumLength(1000).When(x => x.Message is not null)
            .WithMessage("Message cannot exceed 1000 characters");
    }
}