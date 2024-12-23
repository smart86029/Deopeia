namespace Deopeia.Trading.Application.Contracts.GetContract;

public class GetContractViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public UnderlyingType UnderlyingType { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal PricePrecision { get; set; }

    public decimal TickSize { get; set; }

    public decimal ContractSizeQuantity { get; set; }

    public string ContractSizeUnitCode { get; set; } = string.Empty;

    public ICollection<decimal> Leverages { get; set; } = [];

    public ICollection<ContractLocaleDto> Locales { get; set; } = [];
}
