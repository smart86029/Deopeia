using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

internal class DateTimeOffsetConverter()
    : ValueConverter<DateTimeOffset, DateTime>(
        dateTimeOffset => dateTimeOffset.UtcDateTime,
        dateTime => new DateTimeOffset(dateTime))
{
}
