using CarManagementService.Presentation.Models.DTOs.Common;
using FluentValidation;

namespace CarManagementService.Presentation.Infrastructure.Validators.Common;

public class PaginationRequestDTOValidator : AbstractValidator<PaginationRequestDTO>
{
    public PaginationRequestDTOValidator()
    {
        RuleFor(x => x)
            .Must(x => (x.PageNumber.HasValue && x.PageSize.HasValue) || (!x.PageNumber.HasValue && !x.PageSize.HasValue))
            .WithMessage("Both PageNumber and PageSize must be provided together for pagination.");
        
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");
    }
}