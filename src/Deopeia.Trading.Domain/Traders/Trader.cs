namespace Deopeia.Trading.Domain.Traders;

public class Trader : AggregateRoot<TraderId>
{
    private readonly List<Account> _accounts = [];
    private readonly List<TraderSymbol> _traderSymbols = [];

    private Trader() { }

    public Trader(string name, bool isEnabled)
    {
        name.MustNotBeNullOrWhiteSpace();

        Name = name.Trim();
        IsEnabled = isEnabled;
        _accounts.Add(new Account(Id, CurrencyCode.Default, true));
    }

    public string Name { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<TraderSymbol> TraderSymbols => _traderSymbols.AsReadOnly();

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

    public void AddSymbol(Symbol symbol)
    {
        if (_traderSymbols.Any(x => x.Symbol == symbol))
        {
            return;
        }

        _traderSymbols.Add(new TraderSymbol(Id, symbol));
    }

    public void RemoveSymbol(Symbol symbol)
    {
        var traderSymbol = _traderSymbols.FirstOrDefault(x => x.Symbol == symbol);
        if (traderSymbol is null)
        {
            return;
        }

        _traderSymbols.Remove(traderSymbol);
    }
}
