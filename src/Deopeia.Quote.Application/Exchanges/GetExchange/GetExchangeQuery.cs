namespace Deopeia.Quote.Application.Exchanges.GetExchange;

public record GetExchangeQuery(string Mic) : IRequest<GetExchangeViewModel> { }
