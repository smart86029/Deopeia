namespace Deopeia.Trading.Domain.Assets;

public readonly record struct AssetId(Guid Guid) : IEntityId
{
    public AssetId()
        : this(GuidUtility.NewGuid()) { }
}
