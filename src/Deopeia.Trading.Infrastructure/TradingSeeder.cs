using Bogus;
using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Contracts;
using Deopeia.Trading.Domain.Positions;
using Currency = Deopeia.Common.Domain.Finance.Currency;
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
        var contracts = GetContracts().ToList();

        context.Set<Currency>().AddRange(currencies);
        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Unit>().AddRange(GetUnits());
        context.Set<Account>().AddRange(accounts);
        context.Set<Contract>().AddRange(contracts);
        context.Set<Position>().AddRange(GetPositions(accounts, contracts));

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

    private IEnumerable<Contract> GetContracts()
    {
        var results = new Contract[]
        {
            Stock("AAPL", "Apple Inc", null, Usd),
            Index("DJI", "Dow Jones Industrial Average Index", null, Usd, 20),
            Index("NDX", "Nasdaq 100 Index", null, Usd, 20),
            Index("SPX", "S&P 500 Index", null, Usd, 50),
            Commodity("XAU", "Gold", null, Usd, new ContractSize(100, new UnitCode("APZ"))),
            Commodity("XAG", "Silver", null, Usd, new ContractSize(5000, new UnitCode("APZ"))),
            Forex("EURUSD", "Euro/U.S. Dollar", null, Usd),
            Forex("USDJPY", "U.S. Dollar/Japanese Yen", null, Usd),
            Forex("GBPUSD", "British Pound/U.S. Dollar", null, Usd),
            Forex("AUDUSD", "Australian Dollar/U.S. Dollar", null, Usd),
            Cryptocurrency("BTC", "Bitcoin", null, Usd),
            Cryptocurrency("ETH", "Ethereum", null, Usd),
        };

        results[00].UpdateName("蘋果", ZHHant);
        results[01].UpdateName("道瓊工業平均指數", ZHHant);
        results[02].UpdateName("納斯達克100指數", ZHHant);
        results[03].UpdateName("標準普爾500指數", ZHHant);
        results[04].UpdateName("黃金", ZHHant);
        results[05].UpdateName("白銀", ZHHant);
        results[06].UpdateName("歐元/美元", ZHHant);
        results[07].UpdateName("美元/日元", ZHHant);
        results[08].UpdateName("英鎊/美元", ZHHant);
        results[09].UpdateName("澳元/美元", ZHHant);
        results[10].UpdateName("比特幣", ZHHant);
        results[11].UpdateName("以太幣", ZHHant);

        return results;

        Contract Stock(string symbol, string name, string? description, CurrencyCode currencyCode)
        {
            return new Contract(
                symbol,
                name,
                description,
                UnderlyingType.Stock,
                currencyCode,
                new ContractSize(100, new UnitCode("NMB")),
                1,
                [1, 2, 5, 10, 25]
            );
        }

        Contract Index(
            string symbol,
            string name,
            string? description,
            CurrencyCode currencyCode,
            decimal amount
        )
        {
            return new Contract(
                symbol,
                name,
                description,
                UnderlyingType.Index,
                currencyCode,
                new ContractSize(amount, new UnitCode("NMB")),
                1,
                [1, 2, 5, 10, 25, 50, 100, 200, 500, 1000]
            );
        }

        Contract Commodity(
            string symbol,
            string name,
            string? description,
            CurrencyCode currencyCode,
            ContractSize contractSize
        )
        {
            return new Contract(
                symbol,
                name,
                description,
                UnderlyingType.Commodity,
                currencyCode,
                contractSize,
                1,
                [1, 2, 5, 10, 25, 50, 100, 200, 500, 1000]
            );
        }

        Contract Forex(string symbol, string name, string? description, CurrencyCode currencyCode)
        {
            return new Contract(
                symbol,
                name,
                description,
                UnderlyingType.Forex,
                currencyCode,
                new ContractSize(100000, new UnitCode("NMB")),
                1,
                [1, 2, 5, 10, 25, 50, 100, 200, 500, 1000]
            );
        }
        Contract Cryptocurrency(
            string symbol,
            string name,
            string? description,
            CurrencyCode currencyCode
        )
        {
            return new Contract(
                symbol,
                name,
                description,
                UnderlyingType.Cryptocurrency,
                currencyCode,
                new ContractSize(100000, new UnitCode("NMB")),
                1,
                [1, 2, 5, 10, 25, 50, 100, 200, 500, 1000]
            );
        }
    }

    private IEnumerable<Position> GetPositions(
        IEnumerable<Account> accounts,
        IEnumerable<Contract> contracts
    )
    {
        return new Faker<Position>()
            .CustomInstantiator(x => new Position(
                x.PickRandom<PositionType>(),
                x.PickRandom(contracts).Id,
                x.Finance.Amount(1, 10, 0) * 1000,
                new Money(Usd, x.Finance.Amount()),
                null,
                null,
                x.PickRandom(accounts).Id
            ))
            .Generate(5);
    }
}
