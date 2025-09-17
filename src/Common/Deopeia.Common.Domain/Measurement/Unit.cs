namespace Deopeia.Common.Domain.Measurement;

public class Unit : AggregateRoot<UnitCode>, ILocalizable<UnitLocalization, UnitCode>
{
    private readonly EntityLocalizationCollection<UnitLocalization, UnitCode> _localizations = [];

    private Unit() { }

    public Unit(string code, string name, string? symbol)
        : base(new UnitCode(code))
    {
        _localizations.Default.UpdateName(name);
        Symbol = symbol;
    }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Symbol { get; private init; }

    public IReadOnlyList<UnitLocalization> Localizations => _localizations;

    public void UpdateName(string name, CultureInfo culture)
    {
        _localizations[culture].UpdateName(name);
    }

    public void RemoveLocalizations(IEnumerable<UnitLocalization> localizations)
    {
        _localizations.RemoveRange(localizations);
    }
}
