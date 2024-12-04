namespace Deopeia.Trading.Application.Contracts.GetContract;

public record GetContractQuery(string Symbol) : IRequest<GetContractViewModel> { }
