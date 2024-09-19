namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

public record GetFuturesContractQuery(string Mic) : IRequest<GetFuturesContractViewModel> { }
