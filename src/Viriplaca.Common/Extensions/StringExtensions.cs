using System.Text.Json;

namespace Viriplaca.Common.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static string ToPascalCase(this string? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        var camelCase = value.ToCamelCase();

        return char.ToUpperInvariant(camelCase[0]) + camelCase[1..];
    }

    public static string ToCamelCase(this string? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return JsonNamingPolicy.CamelCase.ConvertName(value);
    }
}
