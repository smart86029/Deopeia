using Deopeia.Common.Domain.Finance;

namespace Deopeia.Trading.Domain.Traders;

public class Transaction : Entity<TransactionId>
{
    private Transaction() { }

    internal Transaction(AccountId accountId, TransactionType type, Money money)
    {
        TraderId = accountId.TraderId;
        CurrencyCode = accountId.CurrencyCode;
        Type = type;
        Money = money;
    }

    public TraderId TraderId { get; private init; }

    public CurrencyCode CurrencyCode { get; private init; }

    public TransactionType Type { get; private init; }

    public Money Money { get; private set; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;
}
