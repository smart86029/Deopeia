namespace Deopeia.Finance.Bff.Models.Instruments;

public class Instrument
{
    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public UnderlyingType UnderlyingType { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal PricePrecision { get; set; }

    public decimal TickSize { get; set; }

    public ContractSize ContractSize { get; set; } = new();

    public VolumeRestriction VolumeRestriction { get; set; } = new();

    public ICollection<decimal> Leverages { get; set; } = [];
}
