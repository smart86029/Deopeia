using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Trading.Infrastructure;

internal class TradingContextFactory : IDesignTimeDbContextFactory<TradingContext>
{
    public TradingContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TradingContext>();
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

        return new TradingContext(optionsBuilder.Options);
    }
}
