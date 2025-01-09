using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Domain;

public static partial class ValidationExtensions
{
    public static void MustGreaterThan(
        this decimal value,
        decimal comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value < comparison)
        {
            throw new DomainException(
                "Number.GreaterThan",
                new { Property = GetProperty(filePath, valueName), Comparison = comparison }
            );
        }
    }

    public static void MustGreaterThanOrEqualTo(
        this decimal value,
        decimal comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value < comparison)
        {
            throw new DomainException(
                "Number.GreaterThanOrEqualTo",
                new { Property = GetProperty(filePath, valueName), Comparison = comparison }
            );
        }
    }

    public static void MustLessThan(
        this decimal value,
        decimal comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value < comparison)
        {
            throw new DomainException(
                "Number.LessThan",
                new { Property = GetProperty(filePath, valueName), Comparison = comparison }
            );
        }
    }

    public static void MustLessThanOrEqualTo(
        this decimal value,
        decimal comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value < comparison)
        {
            throw new DomainException(
                "Number.LessThanOrEqualTo",
                new { Property = GetProperty(filePath, valueName), Comparison = comparison }
            );
        }
    }

    public static void MustNotBeNullOrWhiteSpace(
        this string? value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null
    )
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw NewDomainException("String.NotEmpty", filePath, valueName);
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
            throw NewDomainException("Date.OnOrBefore", filePath, valueName, comparisonName);
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
            throw NewDomainException("Date.OnOrBeforeNow", filePath, valueName);
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
            throw NewDomainException("Date.AfterNow", filePath, valueName);
        }
    }

    public static void MustEqualTo(
        this CurrencyCode value,
        CurrencyCode comparison,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null,
        [CallerArgumentExpression(nameof(comparison))] string? comparisonName = null
    )
    {
        if (value != comparison)
        {
            throw NewDomainException("String.EqualTo", filePath, valueName, comparisonName);
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
            throw NewDomainException("String.NotEmpty", filePath, valueName);
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
                throw NewDomainException("Enum.Defined", filePath, valueName);
            }
        }
        else
        {
            if (!value.IsDefined())
            {
                throw NewDomainException("Enum.Defined", filePath, valueName);
            }
        }
    }

    private static DomainException NewDomainException(
        string code,
        string filePath,
        string? valueName
    )
    {
        return new DomainException(code, new { Property = GetProperty(filePath, valueName) });
    }

    private static DomainException NewDomainException(
        string code,
        string filePath,
        string? valueName,
        string? comparisonName
    )
    {
        return new DomainException(
            code,
            new
            {
                Property = GetProperty(filePath, valueName),
                Comparison = GetProperty(filePath, comparisonName),
            }
        );
    }

    private static LocalizableProperty GetProperty(string filePath, string? valueName)
    {
        var modelName = ModelNameRegex().Match(filePath).Value;

        return new LocalizableProperty(modelName, valueName.ToPascalCase());
    }

    [GeneratedRegex(@"(?<=.*\\)(\w+)(?=\.cs)")]
    private static partial Regex ModelNameRegex();
}
