namespace Deopeia.Trading.Application.Accounts.CreateAccount;

public record CreateAccountCommand(string AccountNumber, bool IsEnabled, string CurrencyCode)
    : IRequest { }
