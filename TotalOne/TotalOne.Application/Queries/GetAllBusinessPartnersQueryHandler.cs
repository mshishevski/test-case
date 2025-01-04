using System.Linq.Expressions;

using MediatR;

using TotalOne.Application.Extensions;
using TotalOne.Domain.Base;
using TotalOne.Domain.Entities;
using TotalOne.Domain.Result;
using TotalOne.Domain.SortBy;

namespace TotalOne.Application.Queries;

public record GetAllBusinessPartnersQuery(int PageIndex, int PageSize, bool DescendingSortDirection, BusinessPartnersSortByEnum SortBy) :
        PagedRequest<BusinessPartnersSortByEnum>(PageIndex, PageSize, DescendingSortDirection, SortBy),
        IRequest<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>>;

public record BusinessPartnerQueryResult(int BusinessPartnerId, DateTime LastUpdate);


public class GetAllBusinessPartnersQueryHandler : IRequestHandler<GetAllBusinessPartnersQuery, QueryResult<BaseTableResult<BusinessPartnerQueryResult>>>
{
    private readonly ITotalOneContext _totalOneContext;

    public GetAllBusinessPartnersQueryHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>> Handle(GetAllBusinessPartnersQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = "SELECT * FROM BusinessPartner";

        var orderByClause = DapperExtensions.BuildOrderByClause<BusinessPartner>(GetSortByLastUpdate(), query.DescendingSortDirection);

        using var connection = _totalOneContext.CreateConnection();

        var result = await connection.ApplyPagingAsync<BusinessPartnerQueryResult, BusinessPartner>(
            baseQuery,
            orderByClause,
            query.PageIndex,
            query.PageSize,
            mapper: x => new BusinessPartnerQueryResult(x.BusinessPartnerId, x.LastUpdate)
        );

        return QueryResult.Success(result);
    }

    private static Expression<Func<BusinessPartner, object>> GetSortByLastUpdate()
    {
        return bp => bp.LastUpdate;
    }
}
