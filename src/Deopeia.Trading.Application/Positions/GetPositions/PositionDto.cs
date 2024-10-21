using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Application.Positions.GetPositions;

public class PositionDto
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public Guid InstrumentId { get; set; }

    public OrderSide Side { get; set; }

    public decimal Volume { get; private set; }

    public string Currency { get; set; } = string.Empty;

    public decimal Margin { get; private set; }

    public decimal OpenPrice { get; private set; }

    public DateTimeOffset OpenedAt { get; private set; }
}
