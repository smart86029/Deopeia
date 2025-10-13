namespace Deopeia.Funds.Domain.Accounts;

public sealed class Balance : Entity<BalanceId>
{
    private Balance() { }

    internal Balance(AccountId accountId, AssetCode assetCode)
        : base(new BalanceId(accountId, assetCode)) { }

    public AccountId AccountId => Id.AccountId;

    public AssetCode AssetCode => Id.AssetCode;

    public Amount Available { get; private set; }

    public Amount Locked { get; private set; }

    public Amount Total => Available + Locked;

    internal void Credit(Amount amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value);

        Available += amount;
    }

    internal void Debit(Amount amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value);
        if (Available < amount)
        {
            throw new InvalidOperationException("INSUFFICIENT_AVAILABLE");
        }

        Available -= amount;
    }

    internal void Lock(Amount amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value);
        if (Available < amount)
        {
            throw new InvalidOperationException("INSUFFICIENT_AVAILABLE");
        }

        Available -= amount;
        Locked += amount;
    }

    internal void Unlock(Amount amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value);
        if (Locked < amount)
        {
            throw new InvalidOperationException("INSUFFICIENT_LOCKED");
        }

        Locked -= amount;
        Available += amount;
    }

    internal void ConsumeLocked(Amount amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value);
        if (Locked < amount)
        {
            throw new InvalidOperationException("INSUFFICIENT_LOCKED");
        }

        Locked -= amount;
    }
}
