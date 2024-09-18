namespace Deopeia.Quote.Domain.Assets;

public class Asset : AggregateRoot<AssetId>, ILocalizable<AssetLocale, AssetId>
{
    private readonly EntityLocaleCollection<AssetLocale, AssetId> _locales = [];

    private Asset() { }

    public Asset(string code, string name, string? description)
    {
        code.MustNotBeNullOrWhiteSpace();

        Code = code.Trim();
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
    }

    public string Code { get; private set; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public IReadOnlyCollection<AssetLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void RemoveLocales(IEnumerable<AssetLocale> locales)
    {
        _locales.Remove(locales);
    }
}
