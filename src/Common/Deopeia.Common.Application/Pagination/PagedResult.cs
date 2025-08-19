namespace Deopeia.Common.Application.Pagination;

public class PagedResult<TItem>
{
    private PagedResult() { }

    public PagedResult(PagedQuery<TItem> query, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(query.PageIndex);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(query.PageSize);
        ArgumentOutOfRangeException.ThrowIfNegative(totalCount);

        var pageSize = query.PageSize;
        var pageCount = Math.Max(1, Math.Ceiling(totalCount.ToDecimal() / pageSize).ToInt());
        var pageIndex = Math.Min(Math.Max(1, query.PageIndex), pageCount);

        PageIndex = pageIndex;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalCount = totalCount;
    }

    public int PageIndex { get; private init; }

    public int PageSize { get; private init; }

    public int PageCount { get; private init; }

    public int TotalCount { get; private init; }

    public IReadOnlyList<TItem> Items { get; set; } = [];
}
