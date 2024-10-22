namespace Deopeia.Trading.Application.Positions.GetPosition;

public record GetPositionQuery(Guid Id) : IRequest<GetPositionViewModel> { }
