using FluentValidation;
using MediatR;
using TotalOne.Domain.Result;

namespace TotalOne.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TRequest : IRequest<TResult>
        where TResult : BaseResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var errors = new List<ValidationError>();

        foreach (var validator in _validators)
        {
            var validatorErrors = await validator.ValidateAsync(request, cancellationToken);
            errors.AddRange(
                validatorErrors.Errors
                    .Where(validationFailure => validationFailure is not null)
                    .Select(validationFailure =>
                        new ValidationError(validationFailure.PropertyName, validationFailure.ErrorMessage))
                    .Distinct()
            );
        }

        if (errors.Any())
            throw new ValidationException("A validation error occured.");

        return await next();
    }
}
