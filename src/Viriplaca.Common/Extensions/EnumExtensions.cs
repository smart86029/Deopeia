using System.Reflection;

namespace Viriplaca.Common.Extensions;

public static class EnumExtensions
{
    public static bool IsDefined<TEnum>(this TEnum @enum)
        where TEnum : Enum
    {
        return Enum.IsDefined(typeof(TEnum), @enum);
    }

    public static bool IsFlags<TEnum>(this TEnum _)
        where TEnum : Enum
    {
        return typeof(TEnum).GetCustomAttributes<FlagsAttribute>().Any();
    }
}
