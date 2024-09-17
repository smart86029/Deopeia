namespace Deopeia.Trading.Application.Strategies.GetStrategy;

public record GetStrategyQuery(Guid Id) : IRequest<GetStrategyViewModel> { }
