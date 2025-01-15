using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TraderFavoriteConfiguration : IEntityTypeConfiguration<TraderFavorite>
{
    public void Configure(EntityTypeBuilder<TraderFavorite> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.TraderId, x.Symbol });
    }
}
