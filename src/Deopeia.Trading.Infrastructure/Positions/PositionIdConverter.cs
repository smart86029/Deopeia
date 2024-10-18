using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Infrastructure.Positions;

internal class PositionIdConverter()
    : ValueConverter<PositionId, Guid>(id => id.Guid, guid => new PositionId(guid)) { }
