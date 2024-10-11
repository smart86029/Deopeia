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
        CurrencyCode currencyCode
    )
    {
        type.MustBeDefined();

        Type = type;
        ExchangeId = exchangeId;
        Symbol = symbol;
        _locales.Default.UpdateName(name);
        CurrencyCode = currencyCode;
    }

    public InstrumentType Type { get; private init; }

    public ExchangeId ExchangeId { get; private init; }

    public string Symbol { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public CurrencyCode CurrencyCode { get; private init; }

    public IReadOnlyCollection<InstrumentLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void RemoveLocales(IEnumerable<InstrumentLocale> locales)
    {
        _locales.Remove(locales);
    }
}
