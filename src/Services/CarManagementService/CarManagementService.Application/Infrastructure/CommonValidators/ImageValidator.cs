using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.Infrastructure.CommonValidators;

public class ImageValidator : AbstractValidator<IFormFile>
{
    public ImageValidator()
    {
        RuleFor(x => x.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024)
            .WithMessage("Image size must not exceed 5MB.");
        
        RuleFor(x => x.ContentType)
            .Must(x => x.Equals("image/jpeg") || x.Equals("image/png"))
            .WithMessage("Only JPEG and PNG images are allowed.");
    }
}