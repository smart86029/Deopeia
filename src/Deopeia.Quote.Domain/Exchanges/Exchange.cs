namespace Deopeia.Quote.Domain.Exchanges;

public class Exchange : AggregateRoot
{
    private readonly EntityLocaleCollection<ExchangeLocale> _locales = [];

    private Exchange() { }

    public Exchange(string code)
    {
        Code = code;
    }

    public string Code { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }
}
