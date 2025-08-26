namespace Deopeia.Common.Bff.Pagination;

public abstract record PagedRequest
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }
}
