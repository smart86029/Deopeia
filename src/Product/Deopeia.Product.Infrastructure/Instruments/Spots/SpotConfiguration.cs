using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Infrastructure.Instruments.Spots;

internal sealed class SpotConfiguration : IEntityTypeConfiguration<Spot>
{
    public void Configure(EntityTypeBuilder<Spot> builder) { }
}
