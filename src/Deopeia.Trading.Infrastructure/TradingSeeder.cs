using Deopeia.Trading.Domain.Assets;

namespace Deopeia.Trading.Infrastructure;

public class TradingSeeder : DbSeeder<TradingContext>
{
    public override async Task SeedAsync(TradingContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Asset>().AddRange(GetAssets());

        await context.SaveChangesAsync();
    }

    private IEnumerable<LocaleResource> GetLocaleResources()
    {
        var en = CultureInfo.GetCultureInfo("en");
        var resourcesEN = new LocaleResource[] { };

        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        var resourcesZHHant = new LocaleResource[] { };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

        return results;
    }

    private IEnumerable<Asset> GetAssets()
    {
        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        var results = new Asset[]
        {
            new("XAU", "Gold", null),
            new("XAG", "Silver", null),
            new("XPD", "Palladium", null),
            new("XPT", "Platinum", null),
        };

        results[0].UpdateName("黃金", zhHant);
        results[1].UpdateName("白銀", zhHant);
        results[2].UpdateName("鈀金", zhHant);
        results[3].UpdateName("鉑金", zhHant);

        return results;
    }
}
