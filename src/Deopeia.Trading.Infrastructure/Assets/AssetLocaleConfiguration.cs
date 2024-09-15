using Deopeia.Trading.Domain.Assets;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class AssetLocaleConfiguration : EntityLocaleConfiguration<Asset, AssetLocale, AssetId>
{
    public override void Configure(EntityTypeBuilder<AssetLocale> builder)
    {
        base.Configure(builder);
    }
}
