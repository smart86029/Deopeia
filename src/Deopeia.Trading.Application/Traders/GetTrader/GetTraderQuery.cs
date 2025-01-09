namespace Deopeia.Trading.Application.Traders.GetTrader;

public record GetTraderQuery(Guid Id) : IRequest<GetTraderViewModel> { }
