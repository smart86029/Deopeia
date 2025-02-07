namespace Deopeia.Trading.Application.Traders.GetAccounts;

public record GetAccountsQuery(Guid TraderId) : IRequest<ICollection<AccountDto>> { }
