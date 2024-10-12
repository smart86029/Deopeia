namespace Deopeia.Trading.Application.Accounts.GetAccount;

public class GetAccountViewModel
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }
}
