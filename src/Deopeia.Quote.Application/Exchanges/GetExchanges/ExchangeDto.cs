namespace Deopeia.Quote.Application.Exchanges.GetExchanges;

public class ExchangeDto
{
    public string Mic { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Abbreviation { get; set; }

    public string TimeZone { get; set; } = string.Empty;
}
