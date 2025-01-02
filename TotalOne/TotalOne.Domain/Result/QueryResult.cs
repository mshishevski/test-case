namespace TotalOne.Domain.Result;

public static class QueryResult
{
    public static QueryResult<TResult> Success<TResult>(TResult result) => new(result);
    public static QueryResult<EmptyResult> Success() => new(EmptyResult.Default);
    public static QueryResult<TResult> Failure<TResult>(ErrorType errorType) => new(default, errorType);
    public static QueryResult<TResult> Failure<TResult>(ErrorType errorType, string errorMessage) => Failure<TResult>(default, errorType, errorMessage);
    public static QueryResult<TResult> Failure<TResult>(TResult? result, ErrorType errorType, string errorMessage) => new(result, errorType, errorMessage);
}

public class QueryResult<TResult> : BaseResult<TResult>
{
    internal QueryResult(TResult? result, ErrorType errorType = ErrorType.None, string? errorMessage = default) : base(result, errorType, errorMessage) { }
}
