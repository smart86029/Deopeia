namespace Deopeia.Common.Application.Pagination;

public abstract record PagedQuery<TItem> : IQuery<PagedResult<TItem>>
{
    private int _pageIndex = 1;
    private int _pageSize = 10;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = Math.Max(1, value);
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value.IsBetween(1, 100) ? value : _pageSize;
    }

    public PagedResult<TItem> ToResult(int totalCount)
    {
        return new PagedResult<TItem>(this, totalCount);
    }
}
