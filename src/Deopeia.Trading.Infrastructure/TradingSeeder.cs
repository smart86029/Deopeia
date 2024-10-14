using Unit = Deopeia.Common.Domain.Measurement.Unit;

namespace Deopeia.Trading.Infrastructure;

public class TradingSeeder : DbSeeder<TradingContext>
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");

    public override async Task SeedAsync(TradingContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        context.Set<Currency>().AddRange(GetCurrencies());
        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Unit>().AddRange(GetUnits());

        await context.SaveChangesAsync();
    }

    private IEnumerable<LocaleResource> GetLocaleResources()
    {
        var resourcesEN = new LocaleResource[]
        {
            FromModel(EN, "Strategy.OpenExpression", "Open Expression"),
            FromModel(EN, "Strategy.CloseExpression", "Close Expression"),
            FromError(EN, "Strategy.Expression", "{Property} is invalid."),
        };

        var resourcesZHHant = new LocaleResource[]
        {
            FromModel(ZHHant, "Strategy.OpenExpression", "開倉表達式"),
            FromModel(ZHHant, "Strategy.CloseExpression", "關倉表達式"),
            FromError(ZHHant, "Strategy.Expression", "{Property}不合法。"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

        return results;
    }
}
