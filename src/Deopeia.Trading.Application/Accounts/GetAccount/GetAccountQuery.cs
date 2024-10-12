namespace Deopeia.Trading.Application.Accounts.GetAccount;

public record GetAccountQuery(Guid Id) : IRequest<GetAccountViewModel> { }
