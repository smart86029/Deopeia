using System.Runtime.CompilerServices;

namespace Viriplaca.Common.Domain;

public static class ValidationExtensions
{
    public static void MustNotBeNullOrWhiteSpace(
        this string? value,
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new DomainException("String.NotEmpty", new { Name = valueName.ToPascalCase() });
        }
    }

    public static void MustBeOnOrBeforeNow(
        this DateOnly value,
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainException("Date.OnOrBeforeNow", new { Name = valueName.ToPascalCase() });
        }
    }

    public static void MustBeAfterNow(
        this DateTimeOffset value,
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
    {
        if (value <= DateTimeOffset.UtcNow)
        {
            throw new DomainException("Date.AfterNow", new { Name = valueName.ToPascalCase() });
        }
    }

    public static void MustBeDefined<TEnum>(
        this TEnum value,
        [CallerArgumentExpression(nameof(value))] string? valueName = null)
        where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new DomainException("Enum.Defined", new { Name = valueName.ToPascalCase() });
        }
    }
}
