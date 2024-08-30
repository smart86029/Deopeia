namespace Deopeia.Quote.Domain.Exchanges;

public readonly record struct ExchangeId(string Mic) : IEntityId { }
