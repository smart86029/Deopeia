namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

public record GetFuturesContractQuery(Guid Id) : IRequest<GetFuturesContractViewModel> { }
