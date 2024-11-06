namespace Deopeia.Finance.Bff.Models;

public abstract record PageQuery
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
