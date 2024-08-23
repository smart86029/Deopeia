namespace Deopeia.Quote.Application.Exchanges.GetExchanges;

public class ExchangeDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }
}
