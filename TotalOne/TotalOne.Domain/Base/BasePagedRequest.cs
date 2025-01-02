namespace TotalOne.Domain.Base;

public class BasePagedRequest<T> : IPageable, ISortable<T> where T : Enum
{
    public T SortBy { get; set; } = default!;
    public bool DescendingSortDirection { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public BasePagedRequest() { }

    public BasePagedRequest(BasePagedRequest<T> basePagedRequest)
    {
        PageSize = basePagedRequest.PageSize;
        PageIndex = basePagedRequest.PageIndex;
        SortBy = basePagedRequest.SortBy;
        DescendingSortDirection = basePagedRequest.DescendingSortDirection;
    }
}
