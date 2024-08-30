namespace Deopeia.Quote.Application.Candles.ScrapeHistoricalData;

public record ScrapeHistoricalDataCommand(DateOnly Date) : IRequest { }
