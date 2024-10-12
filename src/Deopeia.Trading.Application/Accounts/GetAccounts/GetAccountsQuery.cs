namespace Deopeia.Trading.Application.Accounts.GetAccounts;

public record GetAccountsQuery(bool? IsEnabled) : PageQuery<AccountDto> { }
