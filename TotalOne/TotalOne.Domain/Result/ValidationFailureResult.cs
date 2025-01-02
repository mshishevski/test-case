namespace TotalOne.Domain.Result;

public class ValidationFailureResult : BaseResult
{
    public ValidationError[] Errors { get; } = Array.Empty<ValidationError>();

    private protected ValidationFailureResult(ValidationError[] errors, ErrorType errorType = ErrorType.Validation) : base(errorType, "One or more validation errors occurred")
    {
        Errors = errors;
    }

    public static ValidationFailureResult Create(params ValidationError[] validationErrors)
    {
        ArgumentNullException.ThrowIfNull(validationErrors);

        return validationErrors.Length == 0
            ? throw new ArgumentException(
                "At least one validation error is required to create ValidationFailureResult.",
                nameof(validationErrors))
            : new(validationErrors);
    }
}
