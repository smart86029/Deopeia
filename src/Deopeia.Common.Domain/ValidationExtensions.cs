using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Deopeia.Common.Domain;

public static partial class ValidationExtensions
{
    public static void MustNotBeNullOrWhiteSpace(
        this string? value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new DomainException(
                "String.NotEmpty",
                new { Property = GetProperty(filePath, valueName) }
            );
        }
    }

    public static void MustBeOnOrBefore(
        this TimeOnly value,
        TimeOnly comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null,
        [CallerArgumentExpression(nameof(comparison))] string? comparisonName = null
    )
    {
        if (value > comparison)
        {
            throw new DomainException(
                "Date.OnOrBefore",
                new
                {
                    Property = GetProperty(filePath, valueName),
                    Comparison = GetProperty(filePath, comparisonName)
                }
            );
        }
    }

    public static void MustBeOnOrBeforeNow(
        this DateOnly value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainException(
                "Date.OnOrBeforeNow",
                new { Property = GetProperty(filePath, valueName) }
            );
        }
    }

    public static void MustBeAfterNow(
        this DateTimeOffset value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (!value.IsAfterNow())
        {
            throw new DomainException(
                "Date.AfterNow",
                new { Property = GetProperty(filePath, valueName) }
            );
        }
    }

    public static void MustNotBeEmpty(
        this Guid value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(
                "String.NotEmpty",
                new { Property = GetProperty(filePath, valueName) }
            );
        }
    }

    public static void MustBeDefined<TEnum>(
        this TEnum value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
        where TEnum : Enum
    {
        if (value.IsFlags())
        {
            var all = Enum.GetValues(typeof(TEnum))
                .OfType<dynamic>()
                .Aggregate((e1, e2) => e1 | e2);
            if ((all & value) != value)
            {
                throw new DomainException(
                    "Enum.Defined",
                    new { Property = GetProperty(filePath, valueName) }
                );
            }
        }
        else
        {
            if (!value.IsDefined())
            {
                throw new DomainException(
                    "Enum.Defined",
                    new { Property = GetProperty(filePath, valueName) }
                );
            }
        }
    }

    private static LocalizableProperty GetProperty(string filePath, string? valueName)
    {
        var modelName = ModelNameRegex().Match(filePath).Value;

        return new LocalizableProperty(modelName, valueName.ToPascalCase());
    }

    [GeneratedRegex(@"(?<=.*\\)(\w+)(?=\.cs)")]
    private static partial Regex ModelNameRegex();
}
