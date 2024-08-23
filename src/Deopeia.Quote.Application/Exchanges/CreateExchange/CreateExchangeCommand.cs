namespace Deopeia.Quote.Application.Exchanges.CreateExchange;

public record CreateExchangeCommand : IRequest
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }

    public ICollection<ExchangeLocaleDto> Locales { get; set; } = [];
}
