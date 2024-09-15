using Deopeia.Trading.Domain.Assets;

namespace Deopeia.Trading.Infrastructure.Assets;

internal class AssetIdConverter()
    : ValueConverter<AssetId, Guid>(id => id.Guid, guid => new AssetId(guid)) { }
