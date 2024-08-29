namespace Deopeia.Common.Application.Page;

public class PageResult<TItem>
{
    public PageResult() { }

    public PageResult(PageQuery<TItem> query, int itemCount)
        : this(query.PageIndex, query.PageSize, itemCount) { }

    public PageResult(int pageIndex, int pageSize, int itemCount)
    {
        PageCount = Math.Max(1, Math.Ceiling(itemCount.ToDecimal() / pageSize).ToInt());
        PageIndex = Math.Min(Math.Max(1, pageIndex), PageCount);
        PageSize = pageSize;
        ItemCount = Math.Max(0, itemCount);
    }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }

    public int ItemCount { get; set; }

    public ICollection<TItem> Items { get; set; } = [];

    [JsonIgnore]
    public int Offset => (PageIndex - 1) * PageSize;

    [JsonIgnore]
    public int Limit => PageSize;
}
