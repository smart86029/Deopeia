using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Infrastructure.Strategies;

internal class StrategyLegConfiguration : IEntityTypeConfiguration<StrategyLeg>
{
    public void Configure(EntityTypeBuilder<StrategyLeg> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.StrategyId, x.SerialNumber });

        builder.HasIndex(x => x.OrderId).IsUnique();
    }
}
