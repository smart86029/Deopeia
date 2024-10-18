using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Infrastructure.Positions;

internal class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ComplexProperty(x => x.Margin);

        builder.ComplexProperty(x => x.OpenPrice);
    }
}
