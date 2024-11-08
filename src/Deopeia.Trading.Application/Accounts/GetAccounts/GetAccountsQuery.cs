namespace Deopeia.Trading.Application.Accounts.GetAccounts;

public record GetAccountsQuery(bool? IsEnabled, string? CurrencyCode) : PageQuery<AccountDto> { }
