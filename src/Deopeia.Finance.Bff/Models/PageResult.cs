namespace Deopeia.Finance.Bff.Models;

public class PageResult<TItem>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }

    public int ItemCount { get; set; }

    public ICollection<TItem> Items { get; set; } = [];
}
