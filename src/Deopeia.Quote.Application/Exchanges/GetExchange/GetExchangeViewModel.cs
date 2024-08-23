namespace Deopeia.Quote.Application.Exchanges.GetExchange;

public class GetExchangeViewModel
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }

    public ICollection<ExchangeLocaleDto> Locales { get; set; } = [];
}
