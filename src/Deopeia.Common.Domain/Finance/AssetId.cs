namespace Deopeia.Common.Domain.Finance;

public readonly record struct AssetId(Guid Guid) : IEntityId
{
    public AssetId()
        : this(Guid.CreateVersion7()) { }
}
