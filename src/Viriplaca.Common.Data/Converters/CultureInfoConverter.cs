using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

internal class CultureInfoConverter : ValueConverter<CultureInfo, string>
{
    private static readonly Expression<Func<CultureInfo, string>> _convertTo = x => x.Name;
    private static readonly Expression<Func<string, CultureInfo>> _convertFrom = x => CultureInfo.GetCultureInfo(x);

    public CultureInfoConverter()
        : base(_convertTo, _convertFrom)
    {
    }
}
