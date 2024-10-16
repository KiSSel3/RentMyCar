using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Review.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator(IRentOfferRepository rentOfferRepository)
    {
        RuleFor(x => x.ReviewerId)
            .NotEmpty().WithMessage("Reviewer ID is required.");
        
        RuleFor(x => x.RentOfferId)
            .NotEmpty().WithMessage("Rent offer ID is required.");
        
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        
        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters.");
    }
}