using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

public class UriReadOnlyCollectionConverter()
    : ValueConverter<IReadOnlyCollection<Uri>, string>(
        strings => string.Join(',', strings),
        stringJoin => stringJoin.Split(',', StringSplitOptions.None).Select(x => new Uri(x)).ToList())
{
}
