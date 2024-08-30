namespace Deopeia.Quote.Application.Candles.ScrapeHistoricalData;

public class CandleDto
{
    public string Symbol { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public decimal Open { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Close { get; set; }

    public decimal Volume { get; set; }
}
