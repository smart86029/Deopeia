namespace Deopeia.Trading.Domain.Traders;

public readonly record struct TraderFavoriteId(TraderId TraderId, Symbol Symbol) : IEntityId { }
