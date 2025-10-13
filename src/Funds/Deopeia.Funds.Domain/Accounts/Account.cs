namespace Deopeia.Funds.Domain.Accounts;

public sealed class Account : AggregateRoot<AccountId>
{
    private readonly List<Balance> _balances = [];

    private Account() { }

    public IReadOnlyList<Balance> Balances => _balances;

    public void Credit(AssetCode assetCode, Amount amount, string refType, long refId)
    {
        var balance = GetOrAddBalance(assetCode);
        balance.Credit(amount);
    }

    public void Debit(AssetCode assetCode, Amount amount, string refType, long refId)
    {
        var balance = GetOrAddBalance(assetCode);
        balance.Debit(amount);
    }

    public void Lock(AssetCode assetCode, Amount amount, string reason)
    {
        var balance = GetOrAddBalance(assetCode);
        balance.Lock(amount);
    }

    public void Unlock(AssetCode assetCode, Amount amount, string reason)
    {
        var balance = GetOrAddBalance(assetCode);
        balance.Unlock(amount);
    }

    public void ConsumeLocked(AssetCode assetCode, Amount amount, string refType, long refId)
    {
        var balance = GetOrAddBalance(assetCode);
        balance.ConsumeLocked(amount);
    }

    private Balance GetOrAddBalance(AssetCode assetCode)
    {
        var balance = _balances.FirstOrDefault(b => b.AssetCode == assetCode);
        if (balance is null)
        {
            balance = new Balance(Id, assetCode);
            _balances.Add(balance);
        }

        return balance;
    }
}
