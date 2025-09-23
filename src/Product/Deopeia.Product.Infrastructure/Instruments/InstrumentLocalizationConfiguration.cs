using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Infrastructure.Instruments;

internal class InstrumentLocalizationConfiguration
    : EntityLocalizationConfiguration<Instrument, InstrumentLocalization, InstrumentId>
{
    public override void Configure(EntityTypeBuilder<InstrumentLocalization> builder)
    {
        base.Configure(builder);
    }
}
