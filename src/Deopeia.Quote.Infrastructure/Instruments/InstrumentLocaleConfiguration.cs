using Deopeia.Common.Domain.Finance;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentLocaleConfiguration
    : EntityLocaleConfiguration<Instrument, InstrumentLocale, InstrumentId>
{
    public override void Configure(EntityTypeBuilder<InstrumentLocale> builder)
    {
        base.Configure(builder);
    }
}
