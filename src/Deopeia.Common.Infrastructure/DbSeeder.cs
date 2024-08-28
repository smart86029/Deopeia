namespace Deopeia.Common.Infrastructure;

public abstract class DbSeeder<TContext> : IDbSeeder<TContext>
    where TContext : DbContext
{
    public abstract Task SeedAsync(TContext context);

    protected IEnumerable<LocaleResource> GetCommonLocaleResources()
    {
        var enUS = CultureInfo.GetCultureInfo("en-US");
        var resourcesENUS = new LocaleResource[]
        {
            FromNone(enUS, "Name", "Name"),
            FromError(enUS, "AccessDenied", "Access denied."),
            FromError(enUS, "String.NotEmpty", "{Property} must not be empty."),
            FromError(enUS, "Date.OnOrBefore", "{Property} must be on or before {Comparison}."),
            FromError(enUS, "Date.OnOrBeforeNow", "{Property} must be on or before now."),
            FromError(enUS, "Date.AfterNow", "{Property} must be after now."),
            FromError(enUS, "Enum.Defined", "{Property} must be defined."),
        };

        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        var resourcesZHTW = new LocaleResource[]
        {
            FromNone(zhTW, "Name", "名稱"),
            FromError(zhTW, "AccessDenied", "存取被拒。"),
            FromError(zhTW, "String.NotEmpty", "{Property}不可為空。"),
            FromError(zhTW, "Date.OnOrBefore", "{Property}必須等於或早於{Comparison}。"),
            FromError(zhTW, "Date.OnOrBeforeNow", "{Property}必須等於或早於現在。"),
            FromError(zhTW, "Date.AfterNow", "{Property}必須晚於現在。"),
            FromError(zhTW, "Enum.Defined", "{Property}必須被定義。"),
        };

        return resourcesENUS.Concat(resourcesZHTW);
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
