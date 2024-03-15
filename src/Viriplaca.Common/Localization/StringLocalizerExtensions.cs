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

    public static string GetEnumString(this IStringLocalizer localizer, object value, Type type)
    {
        return localizer.GetString(LocaleResourceType.Enum, $"{type.Name}.{value:D}");
    }

    public static string GetErrorString(this IStringLocalizer localizer, string code)
    {
        return localizer.GetString($"{LocaleResourceType.Error}.{code}");
    }

    public static string GetPropertyString(this IStringLocalizer localizer, LocalizableProperty localizableProperty)
    {
        return localizer.GetPropertyString(localizableProperty.ModelName, localizableProperty.PropertyName);
    }

    public static string GetPropertyString(this IStringLocalizer localizer, Type type, string propertyName)
    {
        return localizer.GetPropertyString(type.GetDisplayName(), propertyName);
    }

    public static string GetPropertyString(this IStringLocalizer localizer, string modelName, string propertyName)
    {
        var replaced = modelName
            .Replace("Model", string.Empty)
            .Replace("Dto", string.Empty);
        var modelString = localizer.GetString(LocaleResourceType.Model, $"{replaced}.{propertyName}");
        if (!modelString.ResourceNotFound)
        {
            return modelString;
        }

        return localizer.GetString(LocaleResourceType.None, propertyName);
    }

    public static string GetErrorString(this IStringLocalizer localizer, string code, object argument)
    {
        return localizer.GetString($"{LocaleResourceType.Error}.{code}", argument);
    }

    private static LocalizedString GetString(this IStringLocalizer localizer, LocaleResourceType type, string code)
    {
        return localizer.GetString($"{type}.{code}");
    }
}
