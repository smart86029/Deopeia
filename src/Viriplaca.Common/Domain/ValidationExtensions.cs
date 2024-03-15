using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Domain;

public static partial class ValidationExtensions
{
    public static void MustNotBeNullOrWhiteSpace(
        this string? value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new DomainException("String.NotEmpty", new { Property = GetProperty(filePath, valueName) });
        }
    }

    public static void MustBeOnOrBeforeNow(
        this DateOnly value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainException("Date.OnOrBeforeNow", new { Property = GetProperty(filePath, valueName) });
        }
    }

    public static void MustBeAfterNow(
        this DateTimeOffset value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value <= DateTimeOffset.UtcNow)
        {
            throw new DomainException("Date.AfterNow", new { Property = GetProperty(filePath, valueName) });
        }
    }

    public static void MustBeDefined<TEnum>(
        this TEnum value,
        [CallerFilePath] string filePath = "",
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
        where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new DomainException("Enum.Defined", new { Property = GetProperty(filePath, valueName) });
        }
    }

    private static LocalizableProperty GetProperty(string filePath, string? valueName)
    {
        var modelName = ModelNameRegex().Match(filePath).Value;

        return new LocalizableProperty(modelName, valueName.ToPascalCase());
    }

    [GeneratedRegex(@".*/(\w)\.cs")]
    private static partial Regex ModelNameRegex();
}
