using Deopeia.Quote.Domain.Assets;

namespace Deopeia.Quote.Infrastructure.Assets;

internal class AssetLocaleConfiguration : EntityLocaleConfiguration<Asset, AssetLocale, AssetId>
{
    public override void Configure(EntityTypeBuilder<AssetLocale> builder)
    {
        base.Configure(builder);
    }
}
