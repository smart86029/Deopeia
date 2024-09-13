namespace Deopeia.Trading.Domain.Instruments;

public abstract class Instrument
    : AggregateRoot<InstrumentId>,
        ILocalizable<InstrumentLocale, InstrumentId>
{
    private readonly EntityLocaleCollection<InstrumentLocale, InstrumentId> _locales = [];

    protected Instrument() { }

    protected Instrument(
        InstrumentType type,
        string symbol,
        string name,
        string? description,
        Currency currency,
        decimal contractSize
    )
    {
        type.MustBeDefined();

        Type = type;
        Symbol = symbol;
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        Currency = currency;
        ContractSize = contractSize;
    }

    public InstrumentType Type { get; private init; }

    public string Symbol { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public Currency Currency { get; private init; }

    public decimal ContractSize { get; private init; }

    public IReadOnlyCollection<InstrumentLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }
}
