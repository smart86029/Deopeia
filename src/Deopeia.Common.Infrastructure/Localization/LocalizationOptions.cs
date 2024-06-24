namespace Deopeia.Common.Infrastructure.Localization;

internal class LocalizationOptions
{
    public CultureInfo FallbackCulture { get; set; } = CultureInfo.GetCultureInfo("en-US");
}
