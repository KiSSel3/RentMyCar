using FluentValidation;
using IdentityService.BLL.Models.DTOs.Requests.Role;

namespace IdentityService.BLL.Infrastructure.Validators;

public class RoleRequestValidator : AbstractValidator<RoleRequestDTO>
{
    public RoleRequestValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .Length(2, 50).WithMessage("Role name must be between 2 and 50 characters.");
    }
}