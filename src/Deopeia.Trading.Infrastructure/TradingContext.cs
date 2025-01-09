using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Positions;
using Deopeia.Trading.Domain.Strategies;
using Deopeia.Trading.Domain.Traders;
using Deopeia.Trading.Infrastructure.Orders;
using Deopeia.Trading.Infrastructure.Positions;
using Deopeia.Trading.Infrastructure.Strategies;
using Deopeia.Trading.Infrastructure.Traders;

namespace Deopeia.Trading.Infrastructure;

public class TradingContext(DbContextOptions<TradingContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<OrderId>().HaveConversion<OrderIdConverter>();

        configurationBuilder.Properties<PositionId>().HaveConversion<PositionIdConverter>();

        configurationBuilder.Properties<StrategyId>().HaveConversion<StrategyIdConverter>();

        configurationBuilder.Properties<TraderId>().HaveConversion<TraderIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
