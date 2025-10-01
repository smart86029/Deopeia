using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Infrastructure;

public sealed class ProductSeeder : DbSeeder
{
    public override void Seed(DbContext context)
    {
        if (context.Set<Instrument>().Any())
        {
            return;
        }

        context.Set<Instrument>().AddRange(GetInstruments());

        context.SaveChanges();
    }

    private static List<Instrument> GetInstruments()
    {
        return
        [
            new Spot(
                new Symbol("BTCUSDT"),
                "Bitcoin / TetherUS",
                "BTC",
                "USDT",
                new PriceConstraints(0.01M),
                new QuantityConstraints(0.00001M, 0.00001M, 1)
            ),
            new Spot(
                new Symbol("ETHUSDT"),
                "Ethereum / TetherUS",
                "ETH",
                "USDT",
                new PriceConstraints(0.01M),
                new QuantityConstraints(0.0001M, 0.0001M, 1)
            ),
        ];
    }
}
