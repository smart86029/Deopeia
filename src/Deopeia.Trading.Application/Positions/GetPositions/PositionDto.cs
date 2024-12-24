using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Application.Positions.GetPositions;

public class PositionDto
{
    public Guid Id { get; set; }

    public PositionType Type { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public decimal Volume { get; private set; }

    public string Currency { get; set; } = string.Empty;

    public decimal Margin { get; private set; }

    public decimal OpenPrice { get; private set; }

    public DateTimeOffset OpenedAt { get; private set; }
}
