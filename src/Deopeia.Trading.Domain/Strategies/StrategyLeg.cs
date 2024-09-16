using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Strategies;

public class StrategyLeg(StrategyId strategyId, int serialNumber)
    : Entity<StrategyLegId>(new StrategyLegId(strategyId, serialNumber))
{
    public StrategyId StrategyId => Id.StrategyId;

    public int SerialNumber => Id.SerialNumber;

    public OrderSide Side { get; private set; }

    public decimal Ticks { get; private set; }

    public decimal Volume { get; private set; }

    public TimeSpan Timeout { get; private set; }

    public int RetryCount { get; private set; }

    public OrderId? OrderId { get; private set; }
}
