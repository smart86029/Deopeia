namespace Deopeia.Quote.Domain.Ohlcvs;

public readonly record struct OhlcvId(string Symbol, DateTimeOffset RecordedAt) : IEntityId { }
