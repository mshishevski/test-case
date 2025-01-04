using System.Text;

using Dapper;

using MediatR;

using TotalOne.Domain.Result;

namespace TotalOne.Application.Queries;

public record FilterBusinessPartnersQuery(
    long? BusinessPartnerId,
    DateTime? LastUpdateStart,
    DateTime? LastUpdateEnd,
    string? Name)
    : IRequest<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>>;

public record FilterBusinessPartnersResult(int BusinessPartnerId, DateTime LastUpdate, string Name);

public class FilterBusinessPartnersQueryHandler : IRequestHandler<FilterBusinessPartnersQuery, QueryResult<BaseTableResult<BusinessPartnerQueryResult>>>
{
    private readonly ITotalOneContext _totalOneContext;

    public FilterBusinessPartnersQueryHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>> Handle(FilterBusinessPartnersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _totalOneContext.CreateConnection();

            var baseQuery = new StringBuilder(@"
                SELECT BusinessPartnerId, LastUpdate, Name 
                FROM BusinessPartner
                WHERE 1=1");

            var parameters = new DynamicParameters();

            if (query.BusinessPartnerId.HasValue)
            {
                baseQuery.Append(" AND BusinessPartnerId = @BusinessPartnerId");
                parameters.Add("BusinessPartnerId", query.BusinessPartnerId.Value);
            }

            if (query.LastUpdateStart.HasValue && query.LastUpdateEnd.HasValue)
            {
                baseQuery.Append(" AND LastUpdate BETWEEN @LastUpdateStart AND @LastUpdateEnd");
                parameters.Add("LastUpdateStart", query.LastUpdateStart.Value);
                parameters.Add("LastUpdateEnd", query.LastUpdateEnd.Value);
            }

            var result = await connection.QueryAsync<BusinessPartnerQueryResult>(baseQuery.ToString(), parameters);
            var resultList = result.ToList();

            var baseTableResult = new BaseTableResult<BusinessPartnerQueryResult>
            {
                TotalEntries = resultList.Count,
                Entries = resultList
            };

            return QueryResult.Success(baseTableResult);
        }
        catch (Exception ex)
        {
            return QueryResult.Failure<BaseTableResult<BusinessPartnerQueryResult>>(ErrorType.BadRequest, ex.Message);
        }
    }
}
