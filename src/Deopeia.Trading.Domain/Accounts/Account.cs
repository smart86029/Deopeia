namespace Deopeia.Trading.Domain.Accounts;

public class Account : AggregateRoot<AccountId>
{
    private Account() { }

    public Account(string accountNumber, bool isEnabled, CurrencyCode currencyCode)
    {
        accountNumber.MustNotBeNullOrWhiteSpace();

        AccountNumber = accountNumber.Trim();
        IsEnabled = isEnabled;
        Balance = new Money(currencyCode, 0);
        Margin = new Money(currencyCode, 0);
    }

    public string AccountNumber { get; private init; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public Money Balance { get; private set; }

    public Money Margin { get; private set; }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}
