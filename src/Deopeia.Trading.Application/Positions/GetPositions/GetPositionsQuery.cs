namespace Deopeia.Trading.Application.Positions.GetPositions;

public record GetPositionsQuery(Guid? OpenedBy, AssetId? AssetId) : PageQuery<PositionDto> { }
