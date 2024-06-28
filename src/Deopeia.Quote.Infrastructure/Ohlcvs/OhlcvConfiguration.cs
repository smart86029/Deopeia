using Deopeia.Quote.Domain.Ohlcvs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deopeia.Identity.Infrastructure.Users;

internal class OhlcvConfiguration : IEntityTypeConfiguration<Ohlcv>
{
    public void Configure(EntityTypeBuilder<Ohlcv> builder)
    {
        builder.HasIndex(x => new { x.Symbol, x.RecordedAt }).IsUnique();
    }
}
