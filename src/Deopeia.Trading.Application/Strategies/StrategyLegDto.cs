using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Application.Strategies;

public class StrategyLegDto
{
    public int SerialNumber { get; set; }

    public OrderSide Side { get; set; }

    public decimal Ticks { get; set; }

    public decimal Volume { get; set; }
}
