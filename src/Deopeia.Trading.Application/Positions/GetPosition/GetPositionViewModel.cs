using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Application.Positions.GetPosition;

public class GetPositionViewModel
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public PositionType Type { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Volume { get; set; }
}
