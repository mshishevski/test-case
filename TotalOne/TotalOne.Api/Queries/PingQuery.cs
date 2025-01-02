namespace TotalOne.Api.Queries;

[ExtendObjectType("Query")]
public class PingQuery
{
    public string GetPing() => "pong";
}
