using FluentValidation.Results;

namespace CarManagementService.Application.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> ValidationErrors { get; }
    
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {
        ValidationErrors = failures
            .GroupBy(validationFailure => validationFailure.PropertyName, validationFailure => validationFailure.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}