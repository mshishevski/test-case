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

public record BusinessPartnerQueryResult(int BusinessPartnerId, DateTime LastUpdate, string Name);


public class GetAllBusinessPartnersQueryHandler : IRequestHandler<GetAllBusinessPartnersQuery, QueryResult<BaseTableResult<BusinessPartnerQueryResult>>>
{
    private readonly ITotalOneContext _totalOneContext;

    public GetAllBusinessPartnersQueryHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>> Handle(GetAllBusinessPartnersQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = "SELECT * FROM BusinessPartners";

        var orderByClause = DapperExtensions.BuildOrderByClause<BusinessPartner>(GetSortBy(query.SortBy), query.DescendingSortDirection);

        using var connection = _totalOneContext.CreateConnection();

        var result = await connection.ApplyPagingAsync<BusinessPartnerQueryResult, BusinessPartner>(
            baseQuery,
            orderByClause,
            query.PageIndex,
            query.PageSize,
            mapper: x => new BusinessPartnerQueryResult(x.BusinessPartnerId, x.LastUpdate, x.Name)
        );

        return QueryResult.Success(result);
    }

    private static Expression<Func<BusinessPartner, object>> GetSortBy(BusinessPartnersSortByEnum sortBy)
    {
        return bp => bp.Name;
    }
}
