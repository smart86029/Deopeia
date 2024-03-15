using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace Viriplaca.Common.Data.Localization;

internal partial class StringLocalizer(
    IEnumerable<LocaleResource> contents,
    LocalizationOptions options)
    : IStringLocalizer
{
    private readonly Regex _keyRegex = KeyRegex();

    private readonly Dictionary<(CultureInfo Culture, string Code), string> _contents =
        contents.ToDictionary(x => (x.Culture, $"{x.Type}.{x.Code}"), x => x.Content);

    private readonly CultureInfo _fallbackCulture = options.FallbackCulture;

    public LocalizedString this[string name]
    {
        get
        {
            var found = TryGetContent(name, out var value);
            value ??= string.Empty;

            return new LocalizedString(name, value, !found);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var found = TryGetContent(name, out var value);
            value ??= string.Empty;

            if (arguments.Length == 0)
            {
                return new LocalizedString(name, value, !found);
            }

            var placeholderValues = ParsePlaceholderValues(arguments.First());
            var formatted = Format(value, placeholderValues);

            return new LocalizedString(name, formatted, !found);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    private bool TryGetContent(string code, out string content)
    {
        var result =
            _contents.TryGetValue((CultureInfo.CurrentCulture, code), out content!) ||
            _contents.TryGetValue((_fallbackCulture, code), out content!);

        if (!result)
        {
            content = code;
        }

        return result;
    }

    private Dictionary<string, object> ParsePlaceholderValues(object argument)
    {
        var results = new Dictionary<string, object>();
        foreach (var property in argument.GetType().GetProperties())
        {
            var name = property.Name;
            var type = property.PropertyType;
            var value = property.GetValue(argument)!;
            if (type.IsEnum)
            {
                var underlyingType = Enum.GetUnderlyingType(type);
                results.Add(name, this.GetEnumString(Convert.ChangeType(value, underlyingType), type));
            }
            else if (value is LocalizableProperty localizableProperty)
            {
                results.Add(name, this.GetPropertyString(localizableProperty));
            }
            else
            {
                results.Add(name, value);
            }
        }

        return results;
    }

    private string Format(string template, Dictionary<string, object> values)
    {
        var result = _keyRegex.Replace(template, match =>
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
        });

        return result;
    }

    [GeneratedRegex("{([^{}:]+)(?::([^{}]+))?}", RegexOptions.Compiled)]
    private static partial Regex KeyRegex();
}
