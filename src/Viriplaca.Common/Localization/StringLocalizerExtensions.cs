using Microsoft.Extensions.Localization;
using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Localization;

public static class StringLocalizerExtensions
{
    public static string GetEnumString<TEnum>(this IStringLocalizer localizer, TEnum @enum)
        where TEnum : Enum
    {
        return localizer.GetString(LocaleResourceType.Enum, $"{typeof(TEnum).Name}.{@enum:D}");
    }

    public static string GetString(this IStringLocalizer localizer, LocaleResourceType type, string code)
    {
        return localizer.GetString($"{type}.{code}");
    }
}
