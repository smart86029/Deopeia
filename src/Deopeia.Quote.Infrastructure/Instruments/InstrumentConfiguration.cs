using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<Stock>(InstrumentType.Stock)
            .HasValue<ExchangeTradedFund>(InstrumentType.ExchangeTradedFund);

        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => new
            {
                x.Type,
                x.ExchangeId,
                x.Symbol,
            })
            .IsUnique();
    }
}
