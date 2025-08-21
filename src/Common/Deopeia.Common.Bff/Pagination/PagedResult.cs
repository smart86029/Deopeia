namespace Deopeia.Common.Bff.Pagination;

public class PagedResult<TItem>
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public int PageCount { get; init; }

    public int TotalCount { get; init; }

    public IReadOnlyList<TItem> Items { get; init; } = [];
}
