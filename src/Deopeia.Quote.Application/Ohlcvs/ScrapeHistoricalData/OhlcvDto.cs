namespace Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;

public class OhlcvDto
{
    public string Symbol { get; set; }

    public DateOnly Date { get; set; }

    public decimal Open { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Close { get; set; }

    public decimal Volume { get; set; }
}
