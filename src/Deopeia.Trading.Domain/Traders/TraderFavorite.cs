namespace Deopeia.Trading.Domain.Traders;

public class TraderFavorite(TraderId traderId, Symbol symbol, int sortOrder)
    : Entity<TraderFavoriteId>(new TraderFavoriteId(traderId, symbol))
{
    public TraderId TraderId => Id.TraderId;

    public Symbol Symbol => Id.Symbol;

    public int SortOrder { get; private set; } = sortOrder;
}
