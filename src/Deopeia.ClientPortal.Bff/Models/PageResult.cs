namespace Deopeia.Finance.Bff.Models;

public class PageResult<TItem>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }

    public int ItemCount { get; set; }

    public ICollection<TItem> Items { get; set; } = [];

    public PageResult<TTarget> MapItem<TTarget>(Func<TItem, TTarget> map)
        where TTarget : notnull
    {
        return new PageResult<TTarget>
        {
            PageIndex = PageIndex,
            PageCount = PageCount,
            PageSize = PageSize,
            ItemCount = ItemCount,
            Items = Items.Select(map.Invoke).ToList(),
        };
    }
}
