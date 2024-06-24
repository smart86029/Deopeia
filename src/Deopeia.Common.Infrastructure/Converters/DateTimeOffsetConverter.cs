using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Deopeia.Common.Infrastructure.Converters;

internal class DateTimeOffsetConverter()
    : ValueConverter<DateTimeOffset, DateTime>(
        dateTimeOffset => dateTimeOffset.UtcDateTime,
        dateTime => new DateTimeOffset(dateTime)
    ) { }
