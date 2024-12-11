namespace Deopeia.Trading.Application.Contracts.CreateContract;

public record CreateContractCommand(
    string Symbol,
    UnderlyingType UnderlyingType,
    string CurrencyCode,
    decimal ContractSizeQuantity,
    string ContractSizeUnitCode,
    decimal TickSize,
    ICollection<decimal> Leverages,
    ICollection<ContractLocaleDto> Locales
) : IRequest { }
