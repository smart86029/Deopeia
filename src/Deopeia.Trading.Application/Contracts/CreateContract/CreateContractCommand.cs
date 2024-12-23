namespace Deopeia.Trading.Application.Contracts.CreateContract;

public record CreateContractCommand(
    string Symbol,
    UnderlyingType UnderlyingType,
    string CurrencyCode,
    decimal PricePrecision,
    decimal TickSize,
    decimal ContractSizeQuantity,
    string ContractSizeUnitCode,
    decimal VolumeMin,
    decimal VolumeMax,
    decimal VolumeStep,
    ICollection<decimal> Leverages,
    ICollection<SessionDto> Sessions,
    ICollection<ContractLocaleDto> Locales
) : IRequest { }
