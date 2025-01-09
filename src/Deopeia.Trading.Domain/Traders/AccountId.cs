namespace Deopeia.Trading.Domain.Traders;

public readonly record struct AccountId(TraderId TraderId, CurrencyCode CurrencyCode)
    : IEntityId { }
