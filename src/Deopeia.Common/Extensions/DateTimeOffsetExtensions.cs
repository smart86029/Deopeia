namespace Deopeia.Common.Extensions;

public static class DateTimeOffsetExtensions
{
    public static bool IsBeforeNow(this DateTimeOffset value)
    {
        return value < DateTimeOffset.UtcNow;
    }

    public static bool IsAfterNow(this DateTimeOffset value)
    {
        return value > DateTimeOffset.UtcNow;
    }
}
