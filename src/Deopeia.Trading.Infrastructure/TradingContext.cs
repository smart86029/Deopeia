using Deopeia.Trading.Domain.Assets;
using Deopeia.Trading.Infrastructure.Assets;

namespace Deopeia.Trading.Infrastructure;

public class TradingContext(DbContextOptions<TradingContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<AssetId>().HaveConversion<AssetIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
