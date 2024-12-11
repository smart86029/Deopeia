namespace Deopeia.Trading.Application.Contracts.GetContract;

public class GetContractViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public string UnderlyingAsset { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal TickSize { get; set; }

    public decimal ContractSizeQuantity { get; set; }

    public string ContractSizeUnit { get; set; } = string.Empty;

    public ICollection<decimal> Leverages { get; set; } = [];

    public ICollection<ContractLocaleDto> Locales { get; set; } = [];
}
