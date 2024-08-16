using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<Stock>(MarketType.Stock)
            .HasValue<ExchangeTradedFund>(MarketType.ExchangeTradedFund);

        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => new
            {
                x.Type,
                x.Exchange,
                x.Symbol,
            })
            .IsUnique();
    }
}
