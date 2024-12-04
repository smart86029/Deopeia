namespace Deopeia.Trading.Domain.Contracts;

public class Contract : AggregateRoot<Symbol>, ILocalizable<ContractLocale, Symbol>
{
    private readonly EntityLocaleCollection<ContractLocale, Symbol> _locales = [];

    private Contract() { }

    public Contract(
        string symbol,
        string name,
        string? description,
        UnderlyingType underlyingType,
        CurrencyCode currencyCode,
        ContractSize contractSize,
        decimal tickSize
    )
        : base(new Symbol(symbol))
    {
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        UnderlyingType = underlyingType;
        CurrencyCode = currencyCode;
        ContractSize = contractSize;
        TickSize = tickSize;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public UnderlyingType UnderlyingType { get; private init; }

    public CurrencyCode CurrencyCode { get; private init; }

    public ContractSize ContractSize { get; private init; }

    public decimal TickSize { get; private init; }

    public IReadOnlyCollection<ContractLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void RemoveLocales(IEnumerable<ContractLocale> locales)
    {
        _locales.Remove(locales);
    }
}
