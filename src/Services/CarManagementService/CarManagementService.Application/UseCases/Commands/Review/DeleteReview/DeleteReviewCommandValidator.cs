using CarManagementService.Domain.Specifications.Review;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Review.DeleteReview;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}