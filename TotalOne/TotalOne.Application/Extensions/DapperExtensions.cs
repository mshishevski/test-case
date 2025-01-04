
using System.Data;
using System.Linq.Expressions;
using Dapper;
using TotalOne.Domain.Result;

namespace TotalOne.Application.Extensions;

public static class DapperExtensions
{
    public static async Task<BaseTableResult<TPagedItem>> ApplyPagingAsync<TPagedItem, TEntity>(
    this IDbConnection dbConnection,
    string baseQuery,
    string orderByClause,
    int pageIndex,
    int pageSize,
    Func<TEntity, TPagedItem> mapper,
    object? queryParameters = null)
    where TPagedItem : class
    {
        // Use PostgreSQL-compatible pagination syntax
        var paginatedQuery = $@"
        {baseQuery}
        ORDER BY {orderByClause}
        LIMIT @PageSize OFFSET @Offset;

        -- Get total count for pagination
        SELECT COUNT(1) FROM ({baseQuery}) AS CountQuery;
    ";

        var parameters = new DynamicParameters(queryParameters);
        parameters.Add("Offset", pageSize * pageIndex);
        parameters.Add("PageSize", pageSize);

        using var multi = await dbConnection.QueryMultipleAsync(paginatedQuery, parameters);

        var rawItems = await multi.ReadAsync<TEntity>();
        var pagedItems = rawItems
            .Select(mapper)
            .ToList();

        var totalItems = await multi.ReadFirstAsync<int>();

        return new BaseTableResult<TPagedItem>
        {
            Entries = pagedItems,
            TotalEntries = totalItems
        };
    }


    public static string BuildOrderByClause<T>(
    Expression<Func<T, object>> propertySelector,
    bool descendingSortDirection)
    {

        MemberExpression memberExpression = propertySelector.Body switch
        {
            MemberExpression member => member,
            UnaryExpression unary when unary.Operand is MemberExpression member => member,
            _ => throw new ArgumentException("The property selector must be a member expression.")
        };

        var propertyName = memberExpression.Member.Name;
        return descendingSortDirection ? $"{propertyName} DESC" : $"{propertyName} ASC";
    }
}
