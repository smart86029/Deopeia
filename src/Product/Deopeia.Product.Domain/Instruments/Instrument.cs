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
        PriceConstraints priceConstraints,
        QuantityConstraints quantityConstraints
    )
    {
        Type = type;
        Symbol = symbol;
        _localizations.Default.UpdateName(name);
        BaseAsset = baseAsset;
        QuoteAsset = quoteAsset;
        PriceConstraints = priceConstraints;
        QuantityConstraints = quantityConstraints;
    }

    public InstrumentType Type { get; private init; }

    public Symbol Symbol { get; private set; }

    public string Name =>
        _localizations[CultureInfo.CurrentCulture]?.Name ?? _localizations.Default.Name;

    public string BaseAsset { get; private set; } = string.Empty;

    public string QuoteAsset { get; private set; } = string.Empty;

    public PriceConstraints PriceConstraints { get; private set; }

    public QuantityConstraints QuantityConstraints { get; private set; }

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

    public void ChangePriceConstraints(PriceConstraints priceConstraints)
    {
        PriceConstraints = priceConstraints;
    }

    public void ChangeQuantityConstraints(QuantityConstraints quantityConstraints)
    {
        QuantityConstraints = quantityConstraints;
    }

    public void RemoveLocalizations(IEnumerable<CultureInfo> cultures)
    {
        _localizations.RemoveRange(cultures);
    }
}
