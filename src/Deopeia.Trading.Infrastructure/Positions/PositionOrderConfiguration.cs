using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Infrastructure.Positions;

internal class PositionOrderConfiguration : IEntityTypeConfiguration<PositionOrder>
{
    public void Configure(EntityTypeBuilder<PositionOrder> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.PositionId, x.OrderId });
    }
}
