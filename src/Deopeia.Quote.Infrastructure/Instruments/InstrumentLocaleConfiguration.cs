using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentLocaleConfiguration
    : EntityLocaleConfiguration<Instrument, InstrumentLocale>
{
    public override void Configure(EntityTypeBuilder<InstrumentLocale> builder)
    {
        base.Configure(builder);
    }
}