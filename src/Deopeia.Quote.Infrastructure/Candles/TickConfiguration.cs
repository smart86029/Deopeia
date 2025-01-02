using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Infrastructure.Candles;

internal class TickConfiguration : IEntityTypeConfiguration<Tick>
{
    public void Configure(EntityTypeBuilder<Tick> builder)
    {
        builder.HasKey(x => new { x.Symbol, x.Timestamp });
    }
}
