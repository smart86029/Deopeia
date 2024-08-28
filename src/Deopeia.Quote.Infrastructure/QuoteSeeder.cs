using Deopeia.Common.Localization;
using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure;

public class QuoteSeeder : DbSeeder<QuoteContext>
{
    public override async Task SeedAsync(QuoteContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Exchange>().AddRange(GetExchanges());

        await context.SaveChangesAsync();
    }

    private IEnumerable<LocaleResource> GetLocaleResources()
    {
        var enUS = CultureInfo.GetCultureInfo("en-US");
        var resourcesENUS = new LocaleResource[]
        {
            FromModel(enUS, "Exchange.OpeningTime", "Opening Time"),
            FromModel(enUS, "Exchange.ClosingTime", "Closing Time"),
        };

        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        var resourcesZHTW = new LocaleResource[]
        {
            FromModel(zhTW, "Exchange.OpeningTime", "開盤時間"),
            FromModel(zhTW, "Exchange.ClosingTime", "收盤時間"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesENUS).Concat(resourcesZHTW);

        return results;
    }

    private IEnumerable<Exchange> GetExchanges()
    {
        var exchangeXtai = new Exchange(
            "XTAI",
            "Taiwan Stock Exchange",
            TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"),
            new TimeOnly(9, 0),
            new TimeOnly(13, 30)
        );
        exchangeXtai.UpdateName("臺灣證券交易所", CultureInfo.GetCultureInfo("zh-TW"));

        var results = new[] { exchangeXtai, };

        return results;
    }
}
