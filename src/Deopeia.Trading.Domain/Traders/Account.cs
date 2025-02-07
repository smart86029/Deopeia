namespace Deopeia.Trading.Domain.Traders;

public class Account : Entity<AccountId>
{
    private readonly List<Transaction> _transactions = [];

    internal Account(TraderId traderId, CurrencyCode currencyCode, bool isEnabled)
        : base(new AccountId(traderId, currencyCode))
    {
        IsEnabled = isEnabled;
        Balance = new Money(currencyCode, 0);
    }

    public TraderId TraderId => Id.TraderId;

    public CurrencyCode CurrencyCode => Id.CurrencyCode;

    public bool IsEnabled { get; private set; }

    public Money Balance { get; private set; }

    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

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
        _transactions.Add(new Transaction(Id, TransactionType.Deposit, money));
    }

    public void Withdraw(Money money)
    {
        money.CurrencyCode.MustEqualTo(CurrencyCode);
        money.Amount.MustGreaterThanOrEqualTo(0);
        money.Amount.MustLessThanOrEqualTo(Balance.Amount);
        Balance -= money;
        _transactions.Add(new Transaction(Id, TransactionType.Withdrawal, -money));
    }
}
