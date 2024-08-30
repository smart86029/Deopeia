namespace Deopeia.Quote.Application.Exchanges.GetExchange;

public class GetExchangeViewModel
{
    public string Mic { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }

    public ICollection<ExchangeLocaleDto> Locales { get; set; } = [];
}
