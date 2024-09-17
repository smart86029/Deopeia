using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Strategies;

public class StrategyLeg : Entity<StrategyLegId>
{
    internal StrategyLeg(
        StrategyId strategyId,
        int serialNumber,
        OrderSide side,
        decimal ticks,
        decimal volume
    )
        : base(new StrategyLegId(strategyId, serialNumber))
    {
        side.MustBeDefined();
        volume.MustGreaterThanOrEqualTo(0);

        Side = side;
        Ticks = ticks;
        Volume = volume;
    }

    public StrategyId StrategyId => Id.StrategyId;

    public int SerialNumber => Id.SerialNumber;

    public OrderSide Side { get; private set; }

    public decimal Ticks { get; private set; }

    public decimal Volume { get; private set; }

    public OrderId? OrderId { get; private set; }
}
