namespace Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;

public record ScrapeHistoricalDataCommand(DateOnly Date) : IRequest { }
