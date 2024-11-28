namespace Deopeia.Trading.Domain.MatchingEngines;

public readonly record struct OrderPriority(decimal Price, DateTimeOffset CreatedAt) { }
