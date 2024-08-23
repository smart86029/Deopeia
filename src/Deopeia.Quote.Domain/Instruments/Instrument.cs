using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Domain.Instruments;

public abstract class Instrument
    : AggregateRoot<InstrumentId>,
        ILocalizable<InstrumentLocale, InstrumentId>
{
    private readonly EntityLocaleCollection<InstrumentLocale, InstrumentId> _locales = [];

    protected Instrument() { }

    protected Instrument(MarketType type, ExchangeId exchangeId, string symbol, string name)
    {
        type.MustBeDefined();

        Type = type;
        ExchangeId = exchangeId;
        Symbol = symbol;
        _locales.Default.UpdateName(name);
    }

    public MarketType Type { get; private init; }

    public ExchangeId ExchangeId { get; set; }

    public string Symbol { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public IReadOnlyCollection<InstrumentLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }
}
