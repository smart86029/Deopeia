using Bogus;
using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Positions;
using Unit = Deopeia.Common.Domain.Measurement.Unit;

namespace Deopeia.Trading.Infrastructure;

public class TradingSeeder : DbSeeder
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");
    private static readonly CurrencyCode Usd = new("USD");

    public override void Seed(DbContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        var currencies = GetCurrencies();
        var accounts = GetAccounts(currencies).ToList();

        context.Set<Currency>().AddRange(currencies);
        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Unit>().AddRange(GetUnits());
        context.Set<Account>().AddRange(accounts);
        context.Set<Position>().AddRange(GetPositions(accounts));

        context.SaveChanges();
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
            FromModel(ZHHant, "Strategy.CloseExpression", "平倉表達式"),
            FromError(ZHHant, "Strategy.Expression", "{Property}不合法。"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

        return results;
    }

    private IEnumerable<Account> GetAccounts(IEnumerable<Currency> currencies)
    {
        return new Faker<Account>()
            .CustomInstantiator(x => new Account(
                x.Finance.Account(),
                true,
                x.PickRandom(currencies).Id
            ))
            .Generate(10);
    }

    private IEnumerable<Position> GetPositions(IEnumerable<Account> accounts)
    {
        return new Faker<Position>()
            .CustomInstantiator(x => new Position(
                x.PickRandom<PositionType>(),
                new InstrumentId(Guid.Parse("019352c5-ade4-7125-bc19-28419937a665")),
                x.Finance.Amount(1, 10, 0) * 1000,
                new Money(Usd, x.Finance.Amount()),
                null,
                null,
                x.PickRandom(accounts).Id
            ))
            .Generate(5);
    }
}
