namespace Deopeia.Common.Bff.Pagination;

public abstract record PagedQuery<TItem>(int PageIndex, int PageSize);
