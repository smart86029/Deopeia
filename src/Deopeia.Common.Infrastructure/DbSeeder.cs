using Deopeia.Common.Domain.Finance;
using Deopeia.Common.Domain.Measurement;

namespace Deopeia.Common.Infrastructure;

public abstract class DbSeeder
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");

    public abstract void Seed(DbContext context);

    protected static IEnumerable<Currency> GetCurrencies()
    {
        var results = new Currency[]
        {
            new("CNY", "Renminbi", "¥", 2, 7.33101841M),
            new("EUR", "Euro", "€", 2, 0.97060529M),
            new("GBP", "Sterling", "£", 2, 0.812612M),
            new("JPY", "Japanese yen", "¥", 0, 158.14941578M),
            new("TWD", "New Taiwan dollar", "$", 2, 32.91910032M),
            new("USD", "United States dollar", "$", 2, 1),
        };

        results[0].UpdateName("人民幣", ZHHant);
        results[1].UpdateName("歐元", ZHHant);
        results[2].UpdateName("英鎊", ZHHant);
        results[3].UpdateName("日圓", ZHHant);
        results[4].UpdateName("新台幣", ZHHant);
        results[5].UpdateName("美元", ZHHant);

        return results;
    }

    protected static IEnumerable<LocaleResource> GetCommonLocaleResources()
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
            FromError(EN, "String.EqualTo", "{Property} must bet equal to {Comparison}."),
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
            FromError(
                ZHHant,
                "Number.GreaterThanOrEqualTo",
                "{Property}必須大於或等於 {Comparison}。"
            ),
            FromError(ZHHant, "Number.LessThan", "{Property}必須小於 {Comparison}。"),
            FromError(
                ZHHant,
                "Number.LessThanOrEqualTo",
                "{Property}必須小於或等於 {Comparison}。"
            ),
            FromError(ZHHant, "String.EqualTo", "{Property}必須等於{Comparison}。"),
            FromError(ZHHant, "String.NotEmpty", "{Property}不可為空。"),
            FromError(ZHHant, "Date.OnOrBefore", "{Property}必須等於或早於{Comparison}。"),
            FromError(ZHHant, "Date.OnOrBeforeNow", "{Property}必須等於或早於現在。"),
            FromError(ZHHant, "Date.AfterNow", "{Property}必須晚於現在。"),
            FromError(ZHHant, "Enum.Defined", "{Property}必須被定義。"),
        };

        return resourcesEN.Concat(resourcesZHHant);
    }

    protected static IEnumerable<Unit> GetUnits()
    {
        var results = new Unit[]
        {
            new("GRN", "Grain", "gr"),
            new("DWT", "Pennyweight", "dwt"),
            new("APZ", "Troy ounce", "oz t"),
            new("LBR", "Troy pound ", "lb t"),
            new("MGM", "Milligram", "mg"),
            new("GRM", "Gram", "g"),
            new("KGM", "Kilogram", "kg"),
            new("Shares", "Shares", null),
            new("Points", "Points", null),
        };

        results[0].UpdateName("格令", ZHHant);
        results[1].UpdateName("英錢", ZHHant);
        results[2].UpdateName("金衡盎司", ZHHant);
        results[3].UpdateName("金衡磅", ZHHant);
        results[4].UpdateName("毫克", ZHHant);
        results[5].UpdateName("公克", ZHHant);
        results[6].UpdateName("公斤", ZHHant);
        results[7].UpdateName("股", ZHHant);
        results[8].UpdateName("點", ZHHant);

        return results;
    }

    protected static LocaleResource FromNone(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.None, code, content);
    }

    protected static LocaleResource FromEnum<TEnum>(
        CultureInfo culture,
        TEnum @enum,
        string content
    )
    {
        return new LocaleResource(
            culture,
            LocaleResourceType.Enum,
            $"{typeof(TEnum).Name}.{@enum:D}",
            content
        );
    }

    protected static LocaleResource FromModel(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.Model, code, content);
    }

    protected static LocaleResource FromError(CultureInfo culture, string code, string content)
    {
        return new LocaleResource(culture, LocaleResourceType.Error, code, content);
    }
}
