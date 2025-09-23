using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Infrastructure.Instruments;

internal sealed class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.HasDiscriminator(x => x.Type).HasValue<Spot>(InstrumentType.Spot);

        builder.HasIndex(x => x.Symbol).IsUnique();
    }
}
