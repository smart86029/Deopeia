using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure;

public class QuoteSeeder : IDbSeeder<QuoteContext>
{
    public async Task SeedAsync(QuoteContext context)
    {
        if (context.Set<Exchange>().Any())
        {
            return;
        }

        context.Set<Exchange>().AddRange(GetExchanges());

        await context.SaveChangesAsync();
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
