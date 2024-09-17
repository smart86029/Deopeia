using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Infrastructure.Strategies;

internal class StrategyConfiguration : IEntityTypeConfiguration<Strategy>
{
    public void Configure(EntityTypeBuilder<Strategy> builder) { }
}
