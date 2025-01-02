namespace TotalOne.Domain.Base;

public interface ISortable<T> where T : Enum
{
    public T SortBy { get; }
    public bool DescendingSortDirection { get; }
}
