using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Infrastructure.Candles;

internal class CandleConfiguration : IEntityTypeConfiguration<Candle>
{
    public void Configure(EntityTypeBuilder<Candle> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new
        {
            x.Symbol,
            x.TimeFrame,
            x.Timestamp,
        });
    }
}
