namespace Deopeia.Quote.Application.Exchanges.CreateExchange;

public record CreateExchangeCommand(
    string Mic,
    string TimeZone,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime,
    ICollection<ExchangeLocaleDto> Locales
) : IRequest { }
