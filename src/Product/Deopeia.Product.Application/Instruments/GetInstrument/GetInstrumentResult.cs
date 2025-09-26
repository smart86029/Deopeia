using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Application.Instruments.GetInstrument;

public sealed class GetInstrumentResult
{
    public Guid Id { get; set; }

    public InstrumentType Type { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string BaseAsset { get; set; } = string.Empty;

    public string QuoteAsset { get; set; } = string.Empty;

    public int PricePrecision { get; set; }

    public int QuantityPrecision { get; set; }

    public decimal MinQuantity { get; set; }

    public decimal MinNotional { get; set; }

    public IReadOnlyList<InstrumentLocalizationDto> Localizations { get; set; } = [];
}
