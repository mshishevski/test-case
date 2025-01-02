namespace TotalOne.Domain.Base;

public record PagedRequest<T>(int PageIndex, int PageSize, bool DescendingSortDirection, T SortBy) : IPageable, ISortable<T> where T : Enum;
