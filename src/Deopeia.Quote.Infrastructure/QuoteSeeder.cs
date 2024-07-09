using Bogus;
using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Infrastructure;

public class QuoteSeeder : IDbSeeder<QuoteContext>
{
    public async Task SeedAsync(QuoteContext context)
    {
        if (context.Set<Ohlcv>().Any())
        {
            return;
        }

        //context.Set<Ohlcv>().AddRange(GetOhlcvs());

        await context.SaveChangesAsync();
    }

    private IEnumerable<Ohlcv> GetOhlcvs()
    {
        var symbol = "2330.TW";
        var i = 0;
        var results = new Faker<Ohlcv>()
            .CustomInstantiator(x => new Ohlcv(
                symbol,
                DateTime.Today.AddDays(i--),
                x.Finance.Amount(),
                x.Finance.Amount(),
                x.Finance.Amount(),
                x.Finance.Amount(),
                x.Finance.Amount()
            ))
            .GenerateBetween(100, 500);

        return results;
    }
}
