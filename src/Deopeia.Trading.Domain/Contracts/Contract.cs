namespace Deopeia.Trading.Domain.Contracts;

public class Contract : AggregateRoot<Symbol>, ILocalizable<ContractLocale, Symbol>
{
    private readonly List<decimal> _leverages = [];
    private readonly List<Session> _sessions = [];
    private readonly EntityLocaleCollection<ContractLocale, Symbol> _locales = [];

    private Contract() { }

    public Contract(
        string symbol,
        string name,
        string? description,
        UnderlyingType underlyingType,
        CurrencyCode currencyCode,
        decimal pricePrecision,
        decimal tickSize,
        ContractSize contractSize,
        VolumeRestriction volumeRestriction,
        IEnumerable<decimal> leverages
    )
        : base(new Symbol(symbol))
    {
        pricePrecision.MustGreaterThan(0);
        tickSize.MustGreaterThan(0);

        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        UnderlyingType = underlyingType;
        CurrencyCode = currencyCode;
        PricePrecision = pricePrecision;
        TickSize = tickSize;
        ContractSize = contractSize;
        VolumeRestriction = volumeRestriction;
        _leverages.AddRange(leverages);
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public UnderlyingType UnderlyingType { get; private init; }

    public CurrencyCode CurrencyCode { get; private init; }

    public decimal PricePrecision { get; private init; }

    public decimal TickSize { get; private init; }

    public ContractSize ContractSize { get; private init; }

    public VolumeRestriction VolumeRestriction { get; private init; }

    public IReadOnlyCollection<decimal> Leverages => _leverages.AsReadOnly();

    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    public IReadOnlyCollection<ContractLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void AddSession(DayOfWeek dayOfWeek)
    {
        if (_sessions.Any(x => x.DayOfWeek == dayOfWeek)) { }
    }

    public void RemoveLocales(IEnumerable<ContractLocale> locales)
    {
        _locales.Remove(locales);
    }
}
