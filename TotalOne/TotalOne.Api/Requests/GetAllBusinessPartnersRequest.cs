using TotalOne.Domain.Base;
using TotalOne.Domain.SortBy;

namespace TotalOne.Api.Requests;

public class GetAllBusinessPartnersRequest : BasePagedRequest<BusinessPartnersSortByEnum> { }
