namespace Deopeia.Quote.Application.Exchanges.CreateExchange;

public record CreateExchangeCommand(
    string Mic,
    string TimeZone,
    ICollection<ExchangeLocaleDto> Locales
) : IRequest { }
