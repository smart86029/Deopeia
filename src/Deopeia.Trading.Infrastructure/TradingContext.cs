using Deopeia.Trading.Domain.Assets;
using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Strategies;
using Deopeia.Trading.Infrastructure.Assets;
using Deopeia.Trading.Infrastructure.Orders;
using Deopeia.Trading.Infrastructure.Strategies;

namespace Deopeia.Trading.Infrastructure;

public class TradingContext(DbContextOptions<TradingContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<AssetId>().HaveConversion<AssetIdConverter>();
        configurationBuilder.Properties<OrderId>().HaveConversion<OrderIdConverter>();
        configurationBuilder.Properties<StrategyId>().HaveConversion<StrategyIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
