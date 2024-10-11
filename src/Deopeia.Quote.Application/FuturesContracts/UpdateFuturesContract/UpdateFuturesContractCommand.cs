namespace Deopeia.Quote.Application.FuturesContracts.UpdateFuturesContract;

public record UpdateFuturesContractCommand(
    Guid Id,
    DateOnly ExpirationDate,
    ICollection<FuturesContractLocaleDto> Locales
) : IRequest { }
