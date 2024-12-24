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

    public decimal VolumeRestrictionMin { get; set; }

    public decimal VolumeRestrictionMax { get; set; }

    public decimal VolumeRestrictionStep { get; set; }

    public ICollection<decimal> Leverages { get; set; } = [];

    public ICollection<SessionDto> Sessions { get; set; } = [];

    public ICollection<ContractLocaleDto> Locales { get; set; } = [];
}
