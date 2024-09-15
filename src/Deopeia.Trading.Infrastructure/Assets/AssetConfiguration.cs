using Deopeia.Trading.Domain.Assets;

namespace Deopeia.Trading.Infrastructure.Assets;

internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
