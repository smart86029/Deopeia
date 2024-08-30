namespace Deopeia.Quote.Application.Exchanges.UpdateExchange;

public record UpdateExchangeCommand(
    string Mic,
    string TimeZone,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime,
    ICollection<ExchangeLocaleDto> Locales
) : IRequest { }
