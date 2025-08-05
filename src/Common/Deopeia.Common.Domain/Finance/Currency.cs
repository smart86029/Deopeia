namespace Deopeia.Common.Domain.Finance;

public class Currency : AggregateRoot<CurrencyCode>, ILocalizable<CurrencyLocale, CurrencyCode>
{
    private readonly EntityLocaleCollection<CurrencyLocale, CurrencyCode> _locales = [];

    private Currency() { }

    public Currency(string code, string name, string? symbol, int decimals, decimal exchangeRate)
        : base(new CurrencyCode(code))
    {
        _locales.Default.UpdateName(name);
        Symbol = symbol;
        Decimals = decimals;
        ExchangeRate = exchangeRate;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Symbol { get; private init; }

    public int Decimals { get; private init; }

    public decimal ExchangeRate { get; private set; }

    public IReadOnlyCollection<CurrencyLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void RemoveLocales(IEnumerable<CurrencyLocale> locales)
    {
        _locales.Remove(locales);
    }
}
