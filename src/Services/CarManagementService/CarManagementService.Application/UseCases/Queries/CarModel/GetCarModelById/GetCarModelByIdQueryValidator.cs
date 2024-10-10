using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.CarModel.GetCarModelById;

public class GetCarModelByIdQueryValidator : AbstractValidator<GetCarModelByIdQuery>
{
    public GetCarModelByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Car model ID is required.");
    }
}