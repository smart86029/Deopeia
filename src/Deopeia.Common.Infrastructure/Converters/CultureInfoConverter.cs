using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Deopeia.Common.Infrastructure.Converters;

internal class CultureInfoConverter()
    : ValueConverter<CultureInfo, string>(
        culture => culture.Name,
        name => CultureInfo.GetCultureInfo(name)
    ) { }
