namespace Deopeia.Trading.Domain.Traders;

public readonly record struct TransactionId(Guid Guid) : IEntityId
{
    public TransactionId()
        : this(Guid.CreateVersion7()) { }
}
