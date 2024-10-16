using CarManagementService.Domain.Specifications.Review;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Review.UpdateReview;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
        
        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(1000);
    }
}