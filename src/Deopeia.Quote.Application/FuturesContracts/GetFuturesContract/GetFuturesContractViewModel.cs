namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

public class GetFuturesContractViewModel
{
    public Guid Id { get; set; }

    public string ExchangeId { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string CurrencyCode { get; set; } = string.Empty;

    public Guid UnderlyingAssetId { get; set; }

    public decimal TickSize { get; set; }

    public decimal ContractSizeQuantity { get; set; }

    public string ContractSizeUnitCode { get; set; } = string.Empty;

    public ICollection<FuturesContractLocaleDto> Locales { get; set; } = [];
}
