namespace Deopeia.Quote.Domain.Exchanges;

public class Exchange : AggregateRoot<ExchangeId>, ILocalizable<ExchangeLocale, ExchangeId>
{
    private readonly EntityLocaleCollection<ExchangeLocale, ExchangeId> _locales = [];

    private Exchange() { }

    public Exchange(string mic, string name, string? acronym, TimeZoneInfo timeZone)
        : base(new ExchangeId(mic))
    {
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateAcronym(acronym);
        TimeZone = timeZone;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Acronym => _locales[CultureInfo.CurrentCulture]?.Acronym;

    public TimeZoneInfo TimeZone { get; private init; } = TimeZoneInfo.Utc;

    public IReadOnlyCollection<ExchangeLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateAcronym(string? acronym, CultureInfo culture)
    {
        _locales[culture].UpdateAcronym(acronym);
    }

    public void RemoveLocales(IEnumerable<ExchangeLocale> locales)
    {
        _locales.Remove(locales);
    }
}
