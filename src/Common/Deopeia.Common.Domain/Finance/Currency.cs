namespace Deopeia.Common.Domain.Finance;

public class Currency
    : AggregateRoot<CurrencyCode>,
        ILocalizable<CurrencyLocalization, CurrencyCode>
{
    private readonly EntityLocalizationCollection<
        CurrencyLocalization,
        CurrencyCode
    > _localizations = [];

    private Currency() { }

    public Currency(string code, string name, string? symbol, int decimals, decimal exchangeRate)
        : base(new CurrencyCode(code))
    {
        _localizations.Default.UpdateName(name);
        Symbol = symbol;
        Decimals = decimals;
        ExchangeRate = exchangeRate;
    }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Symbol { get; private init; }

    public int Decimals { get; private init; }

    public decimal ExchangeRate { get; private set; }

    public IReadOnlyList<CurrencyLocalization> Localizations => _localizations;

    public void UpdateName(string name, CultureInfo culture)
    {
        _localizations[culture].UpdateName(name);
    }

    public void RemoveLocalizations(IEnumerable<CultureInfo> cultures)
    {
        _localizations.RemoveRange(cultures);
    }
}
