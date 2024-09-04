namespace Deopeia.Common.Infrastructure;

public abstract class DbSeeder<TContext> : IDbSeeder<TContext>
    where TContext : DbContext
{
    public abstract Task SeedAsync(TContext context);

    protected IEnumerable<LocaleResource> GetCommonLocaleResources()
    {
        var en = CultureInfo.GetCultureInfo("en");
        var resourcesEN = new LocaleResource[]
        {
            FromNone(en, "Name", "Name"),
            FromError(en, "AccessDenied", "Access denied."),
            FromError(en, "String.NotEmpty", "{Property} must not be empty."),
            FromError(en, "Date.OnOrBefore", "{Property} must be on or before {Comparison}."),
            FromError(en, "Date.OnOrBeforeNow", "{Property} must be on or before now."),
            FromError(en, "Date.AfterNow", "{Property} must be after now."),
            FromError(en, "Enum.Defined", "{Property} must be defined."),
        };

        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        var resourcesZHHant = new LocaleResource[]
        {
            FromNone(zhHant, "Name", "名稱"),
            FromError(zhHant, "AccessDenied", "存取被拒。"),
            FromError(zhHant, "String.NotEmpty", "{Property}不可為空。"),
            FromError(zhHant, "Date.OnOrBefore", "{Property}必須等於或早於{Comparison}。"),
            FromError(zhHant, "Date.OnOrBeforeNow", "{Property}必須等於或早於現在。"),
            FromError(zhHant, "Date.AfterNow", "{Property}必須晚於現在。"),
            FromError(zhHant, "Enum.Defined", "{Property}必須被定義。"),
        };

        return resourcesEN.Concat(resourcesZHHant);
    }

    protected LocaleResource FromNone(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.None, code, content);
    }

    protected LocaleResource FromEnum<TEnum>(CultureInfo culture, TEnum @enum, string content)
    {
        return new LocaleResource(
            culture,
            LocaleResourceType.Enum,
            $"{typeof(TEnum).Name}.{@enum:D}",
            content
        );
    }

    protected LocaleResource FromModel(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.Model, code, content);
    }

    protected LocaleResource FromError(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.Error, code, content);
    }
}
