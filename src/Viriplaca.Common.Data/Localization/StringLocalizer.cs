using Microsoft.Extensions.Localization;

namespace Viriplaca.Common.Data.Localization;

internal class StringLocalizer(
    IEnumerable<LocaleResource> contents,
    LocalizationOptions options)
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

            return new LocalizedString(name, value ?? string.Empty, !found);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var found = TryGetContent(name, out var value);

            return new LocalizedString(name, value ?? string.Empty, !found);
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
}
