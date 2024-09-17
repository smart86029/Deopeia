using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Infrastructure.Strategies;

internal class StrategyLocaleConfiguration
    : EntityLocaleConfiguration<Strategy, StrategyLocale, StrategyId>
{
    public override void Configure(EntityTypeBuilder<StrategyLocale> builder)
    {
        base.Configure(builder);
    }
}
