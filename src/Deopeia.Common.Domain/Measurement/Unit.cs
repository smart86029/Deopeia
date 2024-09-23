namespace Deopeia.Common.Domain.Measurement;

public class Unit : AggregateRoot<UnitCode>, ILocalizable<UnitLocale, UnitCode>
{
    private readonly EntityLocaleCollection<UnitLocale, UnitCode> _locales = [];

    private Unit() { }

    public Unit(string code, string name, string? symbol)
        : base(new UnitCode(code))
    {
        _locales.Default.UpdateName(name);
        Symbol = symbol;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Symbol { get; private init; }

    public IReadOnlyCollection<UnitLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void RemoveLocales(IEnumerable<UnitLocale> locales)
    {
        _locales.Remove(locales);
    }
}
