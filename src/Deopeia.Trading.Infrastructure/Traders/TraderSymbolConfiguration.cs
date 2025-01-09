using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TraderSymbolConfiguration : IEntityTypeConfiguration<TraderSymbol>
{
    public void Configure(EntityTypeBuilder<TraderSymbol> builder)
    {
        builder.HasKey(x => new { x.TraderId, x.Symbol });
    }
}
