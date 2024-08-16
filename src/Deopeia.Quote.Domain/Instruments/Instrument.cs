namespace Deopeia.Quote.Domain.Instruments;

public abstract class Instrument
    : AggregateRoot<InstrumentId>,
        ILocalizable<InstrumentLocale, InstrumentId>
{
    private readonly EntityLocaleCollection<InstrumentLocale, InstrumentId> _locales = [];

    protected Instrument() { }

    protected Instrument(MarketType type, string symbol)
    {
        type.MustBeDefined();

        Type = type;
        Symbol = symbol;
    }

    public MarketType Type { get; private init; }

    public Guid Exchange { get; set; }

    public string Symbol { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public IReadOnlyCollection<InstrumentLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }
}
