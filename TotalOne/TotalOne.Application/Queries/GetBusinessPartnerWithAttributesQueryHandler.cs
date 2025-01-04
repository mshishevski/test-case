using Dapper;

using MediatR;

using TotalOne.Domain.Entities;
using TotalOne.Domain.Result;

namespace TotalOne.Application.Queries;

public record GetBusinessPartnerWithAttributesQuery(long BusinessPartnerId) : IRequest<QueryResult<IEnumerable<GetBusinessPartnerWithAttributesQueryResult>>>;

public record GetBusinessPartnerWithAttributesQueryResult(long BusinessPartnerId, DateTime LastUpdate);
public class GetBusinessPartnerWithAttributesQueryHandler : IRequestHandler<GetBusinessPartnerWithAttributesQuery, QueryResult<IEnumerable<GetBusinessPartnerWithAttributesQueryResult>>>
{
    private readonly ITotalOneContext _totalOneContext;

    public GetBusinessPartnerWithAttributesQueryHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<QueryResult<IEnumerable<GetBusinessPartnerWithAttributesQueryResult>>> Handle(GetBusinessPartnerWithAttributesQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = "SELECT * FROM BusinessPartner WHERE BusinessPartnerId = @BusinessPartnerId";

        using var connection = _totalOneContext.CreateConnection();

        var queryResult = await connection.QueryAsync<BusinessPartner>(
            baseQuery,
            new { request.BusinessPartnerId }
        );

        var result = queryResult.Select(x => new GetBusinessPartnerWithAttributesQueryResult(x.BusinessPartnerId, x.LastUpdate));

        return QueryResult.Success(result);
    }
}
