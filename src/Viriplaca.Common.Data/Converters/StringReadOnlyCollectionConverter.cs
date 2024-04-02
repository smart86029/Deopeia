using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

public class StringReadOnlyCollectionConverter()
    : ValueConverter<IReadOnlyCollection<string>, string>(
        strings => string.Join(',', strings.OrderBy(x => x)),
        stringJoin => stringJoin.Split(',', StringSplitOptions.None).ToList())
{
}
