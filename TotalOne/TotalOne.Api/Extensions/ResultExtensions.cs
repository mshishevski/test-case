using Microsoft.AspNetCore.Mvc;

using TotalOne.Domain.Result;

namespace TotalOne.Api.Extensions;

internal static class ResultExtensions
{
    public static ActionResult GetHttpResult<TResult>(this BaseResult<TResult> result)
    {
        return result.IsSuccess
            ? GetSuccessActionResult(result.Result)
            : GetFailureActionResult(result.ErrorType, result.ErrorMessage);
    }

    private static ActionResult GetSuccessActionResult<TResult>(TResult result)
    {
        return typeof(TResult) == typeof(Domain.Result.EmptyResult)
            ? new NoContentResult()
            : new OkObjectResult(result);
    }

    public static ActionResult ValidationFailure(this ControllerBase? _, ValidationError[] validationErrors)
        => GetFailureActionResult(ValidationFailureResult.Create(validationErrors));

    private static ActionResult GetFailureActionResult(ErrorType errorType, string? errorMessage)
    {
        return errorType switch
        {
            ErrorType.BadRequest => new BadRequestObjectResult(errorMessage),
            ErrorType.NotFound => new NotFoundObjectResult(errorMessage),
            ErrorType.Forbidden => new ForbidResult(),
            _ => new ObjectResult(errorMessage) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }

    private static ActionResult GetFailureActionResult(ValidationFailureResult result) =>
        new BadRequestObjectResult(result);
}
