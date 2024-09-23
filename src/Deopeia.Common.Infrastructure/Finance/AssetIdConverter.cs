using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Finance;

internal class AssetIdConverter()
    : ValueConverter<AssetId, Guid>(id => id.Guid, guid => new AssetId(guid)) { }
