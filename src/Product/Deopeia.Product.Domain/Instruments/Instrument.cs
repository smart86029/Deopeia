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

    public Symbol Symbol { get; private set; }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string BaseAsset { get; private set; } = string.Empty;

    public string QuoteAsset { get; private set; } = string.Empty;

    public int PricePrecision { get; private set; }

    public int QuantityPrecision { get; private set; }

    public decimal MinQuantity { get; private set; }

    public decimal MinNotional { get; private set; }

    public IReadOnlyList<InstrumentLocalization> Localizations => _localizations;

    public void Rename(string name, CultureInfo culture)
    {
        _localizations[culture].UpdateName(name);
    }

    public void ChangeSymbol(Symbol symbol)
    {
        Symbol = symbol;
    }

    public void ChangeAssets(string baseAsset, string quoteAsset)
    {
        baseAsset.MustNotBeNullOrWhiteSpace();
        quoteAsset.MustNotBeNullOrWhiteSpace();

        BaseAsset = baseAsset;
        QuoteAsset = quoteAsset;
    }

    public void ChangePrecision(int pricePrecision, int quantityPrecision)
    {
        pricePrecision.MustGreaterThan(0);
        quantityPrecision.MustGreaterThan(0);

        PricePrecision = pricePrecision;
        QuantityPrecision = quantityPrecision;
    }

    public void RemoveLocalizations(IEnumerable<CultureInfo> cultures)
    {
        _localizations.RemoveRange(cultures);
    }
}
