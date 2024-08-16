using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Identity.Infrastructure.Users;

internal class OhlcvConfiguration : IEntityTypeConfiguration<Ohlcv>
{
    public void Configure(EntityTypeBuilder<Ohlcv> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.Symbol, x.RecordedAt });
    }
}
