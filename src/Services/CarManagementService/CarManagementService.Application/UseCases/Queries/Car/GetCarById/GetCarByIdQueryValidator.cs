using FluentValidation;

namespace CarManagementService.Application.UseCases.Queries.Car.GetCarById;

public class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
{
    public GetCarByIdQueryValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("Car ID is required.");
    }
}