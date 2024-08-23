namespace Deopeia.Common.Infrastructure.Converters;

internal class DateTimeOffsetConverter()
    : ValueConverter<DateTimeOffset, DateTimeOffset>(
        dateTimeOffset => dateTimeOffset.ToUniversalTime(),
        dateTimeOffset => dateTimeOffset
    ) { }
