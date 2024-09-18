namespace Deopeia.Quote.Application.Exchanges.UpdateExchange;

public record UpdateExchangeCommand(
    string Mic,
    string TimeZone,
    ICollection<ExchangeLocaleDto> Locales
) : IRequest { }
