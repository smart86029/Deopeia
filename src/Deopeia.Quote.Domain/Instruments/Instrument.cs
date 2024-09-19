namespace Deopeia.Quote.Domain.Instruments;

public abstract class Instrument
    : AggregateRoot<InstrumentId>,
        ILocalizable<InstrumentLocale, InstrumentId>
{
    private readonly EntityLocaleCollection<InstrumentLocale, InstrumentId> _locales = [];

    protected Instrument() { }

    protected Instrument(
        InstrumentType type,
        ExchangeId exchangeId,
        string symbol,
        string name,
        Currency currency
    )
    {
        type.MustBeDefined();

        Type = type;
        ExchangeId = exchangeId;
        Symbol = symbol;
        _locales.Default.UpdateName(name);
        Currency = currency;
    }

    public InstrumentType Type { get; private init; }

    public ExchangeId ExchangeId { get; set; }

    public string Symbol { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public Currency Currency { get; private init; }

    public IReadOnlyCollection<InstrumentLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }
}
