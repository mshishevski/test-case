using MediatR;
using TotalOne.Application.Queries;
using TotalOne.Domain.Result;
using TotalOne.Domain.SortBy;

namespace TotalOne.Api.Queries;

[ExtendObjectType("Query")]
public class BusinessPartnerQuery
{
    private readonly ISender _sender;

    public BusinessPartnerQuery(ISender sender)
    {
        _sender = sender;
    }

    public async Task<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>> GetAllBusinessPartners(
        int pageIndex,
        int pageSize,
        bool descendingSortDirection,
        BusinessPartnersSortByEnum sortBy,
        CancellationToken cancellationToken)
    {
        return await _sender.Send(
            new GetAllBusinessPartnersQuery(pageIndex, pageSize, descendingSortDirection, sortBy),
            cancellationToken);
    }

    public async Task<QueryResult<BaseTableResult<BusinessPartnerQueryResult>>> FilterBusinessPartners(
        int? businessPartnerId,
        DateTime? lastUpdateStart,
        DateTime? lastUpdateEnd,
        string? name,
        CancellationToken cancellationToken)
    {
        return await _sender.Send(
            new FilterBusinessPartnersQuery(businessPartnerId, lastUpdateStart, lastUpdateEnd, name),
            cancellationToken);
    }
}