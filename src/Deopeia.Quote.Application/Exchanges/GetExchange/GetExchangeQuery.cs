namespace Deopeia.Quote.Application.Exchanges.GetExchange;

public record GetExchangeQuery(Guid Id) : IRequest<GetExchangeViewModel> { }
