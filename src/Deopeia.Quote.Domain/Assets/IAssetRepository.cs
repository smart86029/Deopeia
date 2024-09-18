namespace Deopeia.Quote.Domain.Assets;

public interface IAssetRepository : IRepository<Asset, AssetId>
{
    Task<Asset> GetAssetAsync(AssetId assetId);

    void Add(Asset asset);
}
