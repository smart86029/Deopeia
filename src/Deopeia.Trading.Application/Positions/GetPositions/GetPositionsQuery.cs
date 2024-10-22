using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Application.Positions.GetPositions;

public record GetPositionsQuery(PositionType? Type) : PageQuery<PositionDto> { }
