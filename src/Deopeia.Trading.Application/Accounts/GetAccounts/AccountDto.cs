namespace Deopeia.Trading.Application.Accounts.GetAccounts;

public class AccountDto
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public string Currency { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
