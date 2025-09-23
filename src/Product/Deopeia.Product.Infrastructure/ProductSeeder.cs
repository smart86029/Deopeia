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
                2,
                6,
                0.000001M,
                10M
            ),
            new Spot(
                new Symbol("ETHUSDT"),
                "Ethereum / TetherUS",
                "ETH",
                "USDT",
                2,
                6,
                0.000001M,
                10M
            ),
        ];
    }
}
