namespace Deopeia.Trading.Domain.Traders;

public class Account : Entity<AccountId>
{
    internal Account(TraderId traderId, CurrencyCode currencyCode, bool isEnabled)
        : base(new AccountId(traderId, currencyCode))
    {
        IsEnabled = isEnabled;
    }

    public TraderId TraderId => Id.TraderId;

    public CurrencyCode CurrencyCode => Id.CurrencyCode;

    public bool IsEnabled { get; private set; }

    public Money Balance { get; private set; }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void Deposit(Money money)
    {
        money.CurrencyCode.MustEqualTo(CurrencyCode);
        money.Amount.MustGreaterThanOrEqualTo(0);
        Balance += money;
    }

    public void Withdraw(Money money)
    {
        money.CurrencyCode.MustEqualTo(CurrencyCode);
        money.Amount.MustGreaterThanOrEqualTo(0);
        money.Amount.MustLessThanOrEqualTo(Balance.Amount);
        Balance -= money;
    }
}
