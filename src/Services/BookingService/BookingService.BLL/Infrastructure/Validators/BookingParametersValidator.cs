using BookingService.BLL.Models.DTOs.Booking;
using FluentValidation;

namespace BookingService.BLL.Infrastructure.Validators;

public class BookingParametersValidator : AbstractValidator<BookingParametersDTO>
{
    public BookingParametersValidator()
    {
        When(x => x.StartDate.HasValue, () =>
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.EndDate.HasValue)
                .WithMessage("Start date must be less than or equal to end date");
        });

        When(x => x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate.HasValue)
                .WithMessage("End date must be greater than or equal to start date");
        });

        When(x => x.Status.HasValue, () =>
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid booking status");
        });
    }
}