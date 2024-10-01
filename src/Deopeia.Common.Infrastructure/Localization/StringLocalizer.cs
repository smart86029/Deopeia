using Microsoft.Extensions.Localization;

namespace Deopeia.Common.Infrastructure.Localization;

internal class StringLocalizer(IEnumerable<LocaleResource> contents, LocalizationOptions options)
    : IStringLocalizer
{
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
            var formatted = value.Format(placeholderValues);

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
            _contents.TryGetValue((CultureInfo.CurrentCulture, code), out content!)
            || _contents.TryGetValue((_fallbackCulture, code), out content!);

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
                results.Add(
                    name,
                    this.GetEnumString(Convert.ChangeType(value, underlyingType), type)
                );
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
}
