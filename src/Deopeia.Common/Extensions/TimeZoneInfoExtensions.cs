using TimeZoneNames;

namespace Deopeia.Common.Extensions;

public static class TimeZoneInfoExtensions
{
    public static string GetDisplayName(this TimeZoneInfo timeZone)
    {
        return GetDisplayName(timeZone, CultureInfo.CurrentCulture);
    }

    public static string GetDisplayName(this TimeZoneInfo timeZone, CultureInfo culture)
    {
        var languageCode = culture.Name.Replace("zh-Hant", "zh-TW");

        return TZNames.GetDisplayNameForTimeZone(timeZone.Id, languageCode) ?? timeZone.DisplayName;
    }
}
