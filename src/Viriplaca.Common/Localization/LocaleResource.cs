namespace Viriplaca.Common.Localization;

public class LocaleResource
{
    private LocaleResource()
    {
    }

    public LocaleResource(CultureInfo culture, LocaleResourceType type, string code, string content)
    {
        Culture = culture;
        Type = type;
        Code = code;
        Content = content;
    }

    public CultureInfo Culture { get; private set; } = CultureInfo.CurrentCulture;

    public LocaleResourceType Type { get; private set; }

    public string Code { get; private set; } = string.Empty;

    public string Content { get; private set; } = string.Empty;
}
