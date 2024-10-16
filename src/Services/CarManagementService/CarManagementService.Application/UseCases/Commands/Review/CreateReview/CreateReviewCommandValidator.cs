using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using FluentValidation;

namespace CarManagementService.Application.UseCases.Commands.Review.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator(IRentOfferRepository rentOfferRepository)
    {
        RuleFor(x => x.ReviewerId)
            .NotEmpty();
        
        RuleFor(x => x.RentOfferId)
            .NotEmpty();
        
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
        
        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(1000);
    }
}