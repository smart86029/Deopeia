using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Infrastructure.Strategies;

internal class StrategyIdConverter()
    : ValueConverter<StrategyId, Guid>(id => id.Guid, guid => new StrategyId(guid)) { }
