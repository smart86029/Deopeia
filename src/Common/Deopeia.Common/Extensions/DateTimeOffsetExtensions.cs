namespace Deopeia.Common.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset Midnight(this DateTimeOffset value)
    {
        return new DateTimeOffset(value.Year, value.Month, value.Day, 0, 0, 0, value.Offset);
    }

    public static bool IsBeforeNow(this DateTimeOffset value)
    {
        return value < DateTimeOffset.UtcNow;
    }

    public static bool IsAfterNow(this DateTimeOffset value)
    {
        return value > DateTimeOffset.UtcNow;
    }
}
