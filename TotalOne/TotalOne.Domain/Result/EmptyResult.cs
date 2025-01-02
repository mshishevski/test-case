namespace TotalOne.Domain.Result;

public sealed class EmptyResult
{
    private EmptyResult() { }

    public static readonly EmptyResult Default = new();
}
