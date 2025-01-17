namespace Deopeia.Trading.Domain.Traders;

public class Trader : AggregateRoot<TraderId>
{
    private readonly List<Account> _accounts = [];
    private readonly List<TraderFavorite> _traderFavorites = [];

    private Trader() { }

    public Trader(Guid id, string name)
        : base(new TraderId(id))
    {
        name.MustNotBeNullOrWhiteSpace();

        Name = name.Trim();
        _accounts.Add(new Account(Id, CurrencyCode.Default, true));
    }

    public string Name { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; } = true;

    public IReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();

    public IReadOnlyCollection<TraderFavorite> TraderFavorites => _traderFavorites.AsReadOnly();

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

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
        var account = _accounts.FirstOrDefault(x => x.CurrencyCode == money.CurrencyCode);
        if (account is null)
        {
            account = new Account(Id, money.CurrencyCode, true);
            _accounts.Add(account);
        }

        account.Deposit(money);
    }

    public void Withdraw(Money money)
    {
        var account = _accounts.FirstOrDefault(x => x.CurrencyCode == money.CurrencyCode);
        account?.Withdraw(money);
    }

    public void Like(Symbol symbol)
    {
        if (_traderFavorites.Any(x => x.Symbol == symbol))
        {
            return;
        }

        _traderFavorites.Add(new TraderFavorite(Id, symbol, _traderFavorites.Count));
    }

    public void Dislike(Symbol symbol)
    {
        var traderSymbol = _traderFavorites.FirstOrDefault(x => x.Symbol == symbol);
        if (traderSymbol is null)
        {
            return;
        }

        _traderFavorites.Remove(traderSymbol);
    }
}
