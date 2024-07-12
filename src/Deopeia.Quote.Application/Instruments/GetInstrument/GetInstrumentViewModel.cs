namespace Deopeia.Quote.Application.Instruments.GetInstrument;

public class GetInstrumentViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string CurrencyCode { get; set; } = string.Empty;

    public TimeSpan UtcOffset { get; set; } = TimeZoneInfo.Utc.BaseUtcOffset;

    public ExchangeDto Exchange { get; set; } = new();
}
