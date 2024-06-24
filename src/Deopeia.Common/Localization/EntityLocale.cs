namespace Deopeia.Common.Localization;

public abstract class EntityLocale
{
    protected EntityLocale() { }

    protected EntityLocale(CultureInfo culture)
    {
        Culture = culture;
    }

    public Guid EntityId { get; private set; }

    [JsonIgnore]
    public CultureInfo Culture { get; internal set; } = CultureInfo.CurrentCulture;
}
