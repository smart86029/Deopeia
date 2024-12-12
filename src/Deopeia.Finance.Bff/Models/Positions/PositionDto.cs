namespace Deopeia.Finance.Bff.Models.Positions;

public class PositionDto
{
    public Guid Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public string InstrumentId { get; set; } = string.Empty;

    public decimal Volume { get; set; }

    public string Currency { get; set; } = string.Empty;

    public decimal Margin { get; set; }

    public decimal OpenPrice { get; set; }

    public DateTimeOffset OpenedAt { get; set; }

    public decimal Price { get; set; }

    public decimal UnrealisedPnL { get; set; }
}
