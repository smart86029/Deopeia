using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

internal class CultureInfoConverter()
    : ValueConverter<CultureInfo, string>(
        culture => culture.Name,
        name => CultureInfo.GetCultureInfo(name))
{
}
