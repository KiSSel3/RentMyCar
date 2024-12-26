using BookingService.BLL.Models.DTOs.Booking;
using FluentValidation;

namespace BookingService.BLL.Infrastructure.Validators;

public class BookingParametersValidator : AbstractValidator<BookingParametersDTO>
{
    public BookingParametersValidator()
    {
        RuleFor(x => x.StartDateFrom)
            .LessThanOrEqualTo(x => x.StartDateTo)
            .When(x => x.StartDateFrom.HasValue && x.StartDateTo.HasValue)
            .WithMessage("StartDateFrom must be less than or equal to StartDateTo");

        RuleFor(x => x.StartDateTo)
            .GreaterThanOrEqualTo(x => x.StartDateFrom)
            .When(x => x.StartDateFrom.HasValue && x.StartDateTo.HasValue)
            .WithMessage("StartDateTo must be greater than or equal to StartDateFrom");
        
        RuleFor(x => x.EndDateFrom)
            .LessThanOrEqualTo(x => x.EndDateTo)
            .When(x => x.EndDateFrom.HasValue && x.EndDateTo.HasValue)
            .WithMessage("EndDateFrom must be less than or equal to EndDateTo");

        RuleFor(x => x.EndDateTo)
            .GreaterThanOrEqualTo(x => x.EndDateFrom)
            .When(x => x.EndDateFrom.HasValue && x.EndDateTo.HasValue)
            .WithMessage("EndDateTo must be greater than or equal to EndDateFrom");
        
        When(x => x.Status.HasValue, () =>
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid booking status");
        });
    }
}