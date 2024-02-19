using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.EntityFrameworkCore.Converters;

internal class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTime>
{
    private static readonly Expression<Func<DateTimeOffset, DateTime>> _convertTo = x => x.UtcDateTime;
    private static readonly Expression<Func<DateTime, DateTimeOffset>> _convertFrom = x => new DateTimeOffset(x);

    public DateTimeOffsetConverter()
        : base(_convertTo, _convertFrom)
    {
    }
}
