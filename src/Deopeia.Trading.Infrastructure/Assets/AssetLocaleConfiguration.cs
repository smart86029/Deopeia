using Deopeia.Trading.Domain.Assets;

namespace Deopeia.Trading.Infrastructure.Assets;

internal class AssetLocaleConfiguration : EntityLocaleConfiguration<Asset, AssetLocale, AssetId>
{
    public override void Configure(EntityTypeBuilder<AssetLocale> builder)
    {
        base.Configure(builder);
    }
}
