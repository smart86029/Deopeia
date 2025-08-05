using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Deopeia.Common.Extensions;

public static partial class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static string ToPascalCase(this string? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return FirstLetterRegex().Replace(value, x => x.Value.ToUpper());
    }

    public static string ToCamelCase(this string? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return JsonNamingPolicy.CamelCase.ConvertName(value);
    }

    public static string ToSnakeCaseLower(this string? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return JsonNamingPolicy.SnakeCaseLower.ConvertName(value);
    }

    public static string Format(this string template, Dictionary<string, object> values)
    {
        var result = KeyRegex()
            .Replace(
                template,
                match =>
                {
                    var key = match.Groups[1].Value;
                    if (!values.TryGetValue(key, out var value))
                    {
                        return match.Value;
                    }

                    if (match.Groups[2].Success)
                    {
                        var format = match.Groups[2].Value;
                        return string.Format($"{{0:{format}}}", value);
                    }

                    return value?.ToString() ?? string.Empty;
                }
            );

        return result;
    }

    [GeneratedRegex(@"\b\w")]
    private static partial Regex FirstLetterRegex();

    [GeneratedRegex("{([^{}:]+)(?::([^{}]+))?}")]
    private static partial Regex KeyRegex();
}
