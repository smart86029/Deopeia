namespace Deopeia.Product.Domain.Instruments;

public abstract class Instrument
    : AggregateRoot<InstrumentId>,
        ILocalizable<InstrumentLocalization, InstrumentId>
{
    private readonly EntityLocalizationCollection<
        InstrumentLocalization,
        InstrumentId
    > _localizations = [];

    protected Instrument() { }

    public Instrument(
        InstrumentType type,
        Symbol symbol,
        string name,
        string baseAsset,
        string quoteAsset,
        int pricePrecision,
        int quantityPrecision,
        decimal minQuantity,
        decimal minNotional
    )
    {
        pricePrecision.MustGreaterThan(0);
        quantityPrecision.MustGreaterThan(0);
        minQuantity.MustGreaterThan(0);
        minNotional.MustGreaterThan(0);

        Type = type;
        Symbol = symbol;
        _localizations.Default.UpdateName(name);
        BaseAsset = baseAsset;
        QuoteAsset = quoteAsset;
        PricePrecision = pricePrecision;
        QuantityPrecision = quantityPrecision;
        MinQuantity = minQuantity;
        MinNotional = minNotional;
    }

    public InstrumentType Type { get; private init; }

    public Symbol Symbol { get; private init; }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string BaseAsset { get; private init; } = string.Empty;

    public string QuoteAsset { get; private init; } = string.Empty;

    public int PricePrecision { get; private init; }

    public int QuantityPrecision { get; private init; }

    public decimal MinQuantity { get; private init; }

    public decimal MinNotional { get; private init; }

    public IReadOnlyList<InstrumentLocalization> Localizations => _localizations;

    public void UpdateName(string name, CultureInfo culture)
    {
        _localizations[culture].UpdateName(name);
    }

    public void RemoveLocalizations(IEnumerable<InstrumentLocalization> localizations)
    {
        _localizations.RemoveRange(localizations);
    }
}
