using Bogus;
using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Positions;
using Unit = Deopeia.Common.Domain.Measurement.Unit;

namespace Deopeia.Trading.Infrastructure;

public class TradingSeeder : DbSeeder<TradingContext>
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");
    private static readonly CurrencyCode Usd = new("USD");

    public override async Task SeedAsync(TradingContext context)
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
            .RuleFor(x => x.OpenedBy, x => x.PickRandom(accounts).Id)
            .RuleFor(x => x.Type, x => x.PickRandom<PositionType>())
            .RuleFor(x => x.Volume, x => x.Finance.Amount(1, 10, 0) * 1000)
            .RuleFor(x => x.Margin, x => new Money(Usd, x.Finance.Amount()))
            .RuleFor(x => x.OpenPrice, x => new Money(Usd, x.Finance.Amount()))
            .Generate(5);
    }
}
