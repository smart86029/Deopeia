namespace Deopeia.Common.Infrastructure.Converters;

internal class TimeZoneInfoConverter()
    : ValueConverter<TimeZoneInfo, string>(
        timeZone => timeZone.Id,
        id => TimeZoneInfo.FindSystemTimeZoneById(id)
    ) { }
