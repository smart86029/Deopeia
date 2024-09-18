using Deopeia.Quote.Domain.Assets;

namespace Deopeia.Quote.Infrastructure.Assets;

internal class AssetRepository(QuoteContext context) : IAssetRepository
{
    private readonly DbSet<Asset> _assets = context.Set<Asset>();

    public async Task<Asset> GetAssetAsync(AssetId assetId)
    {
        var result = await _assets.Include(x => x.Locales).SingleAsync(x => x.Id == assetId);

        return result;
    }

    public void Add(Asset asset)
    {
        _assets.Add(asset);
    }
}
