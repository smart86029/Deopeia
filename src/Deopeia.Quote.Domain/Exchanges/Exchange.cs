namespace Deopeia.Quote.Domain.Exchanges;

public class Exchange : AggregateRoot<ExchangeId>, ILocalizable<ExchangeLocale, ExchangeId>
{
    private readonly EntityLocaleCollection<ExchangeLocale, ExchangeId> _locales = [];

    private Exchange() { }

    public Exchange(
        string code,
        string name,
        TimeZoneInfo timeZone,
        TimeOnly openingTime,
        TimeOnly closingTime
    )
        : base(new ExchangeId())
    {
        openingTime.MustBeOnOrBefore(closingTime);

        Code = code;
        _locales.Default.UpdateName(name);
        TimeZone = timeZone;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }

    public string Code { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public TimeZoneInfo TimeZone { get; private init; } = TimeZoneInfo.Utc;

    public TimeOnly OpeningTime { get; private set; }

    public TimeOnly ClosingTime { get; private set; }

    public IReadOnlyCollection<ExchangeLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateOpenHours(TimeOnly openingTime, TimeOnly closingTime)
    {
        openingTime.MustBeOnOrBefore(closingTime);

        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }

    public void RemoveLocales(IEnumerable<ExchangeLocale> locales)
    {
        _locales.Remove(locales);
    }
}
