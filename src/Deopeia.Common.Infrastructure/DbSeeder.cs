using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure;

public abstract class DbSeeder<TContext> : IDbSeeder<TContext>
    where TContext : DbContext
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");

    public abstract Task SeedAsync(TContext context);

    protected IEnumerable<Currency> GetCurrencies()
    {
        var results = new Currency[]
        {
            new("CNY", "Renminbi", "¥", 2),
            new("EUR", "Euro", "€", 2),
            new("GBP", "Sterling", "£", 2),
            new("JPY", "Japanese yen", "¥", 0),
            new("TWD", "New Taiwan dollar", "$", 2),
            new("USD", "United States dollar", "$", 2),
        };

        results[0].UpdateName("人民幣", ZHHant);
        results[1].UpdateName("歐元", ZHHant);
        results[2].UpdateName("英鎊", ZHHant);
        results[3].UpdateName("日圓", ZHHant);
        results[4].UpdateName("新台幣", ZHHant);
        results[5].UpdateName("美元", ZHHant);

        return results;
    }

    protected IEnumerable<LocaleResource> GetCommonLocaleResources()
    {
        var resourcesEN = new LocaleResource[]
        {
            FromNone(EN, "Name", "Name"),
            FromError(EN, "AccessDenied", "Access denied."),
            FromError(EN, "Number.GreaterThan", "{Property} must be greater than {Comparison}."),
            FromError(
                EN,
                "Number.GreaterThanOrEqualTo",
                "{Property} must be greater than or equal to {Comparison}."
            ),
            FromError(EN, "Number.LessThan", "{Property} must be less than {Comparison}."),
            FromError(
                EN,
                "Number.LessThanOrEqualTo",
                "{Property} must be less than or equal to {Comparison}."
            ),
            FromError(EN, "String.NotEmpty", "{Property} must not be empty."),
            FromError(EN, "Date.OnOrBefore", "{Property} must be on or before {Comparison}."),
            FromError(EN, "Date.OnOrBeforeNow", "{Property} must be on or before now."),
            FromError(EN, "Date.AfterNow", "{Property} must be after now."),
            FromError(EN, "Enum.Defined", "{Property} must be defined."),
        };

        var resourcesZHHant = new LocaleResource[]
        {
            FromNone(ZHHant, "Name", "名稱"),
            FromError(ZHHant, "AccessDenied", "存取被拒。"),
            FromError(ZHHant, "Number.GreaterThan", "{Property}必須大於 {Comparison}。"),
            FromError(ZHHant, "Number.GreaterThanOrEqualTo", "{Property}必須大於或等於 {Comparison}。"),
            FromError(ZHHant, "Number.LessThan", "{Property}必須小於 {Comparison}。"),
            FromError(ZHHant, "Number.LessThanOrEqualTo", "{Property}必須小於或等於 {Comparison}。"),
            FromError(ZHHant, "String.NotEmpty", "{Property}不可為空。"),
            FromError(ZHHant, "Date.OnOrBefore", "{Property}必須等於或早於{Comparison}。"),
            FromError(ZHHant, "Date.OnOrBeforeNow", "{Property}必須等於或早於現在。"),
            FromError(ZHHant, "Date.AfterNow", "{Property}必須晚於現在。"),
            FromError(ZHHant, "Enum.Defined", "{Property}必須被定義。"),
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
