using Deopeia.Quote.Domain.Assets;

namespace Deopeia.Quote.Infrastructure.Assets;

internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
