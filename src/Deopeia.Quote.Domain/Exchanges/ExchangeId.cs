namespace Deopeia.Quote.Domain.Exchanges;

public readonly record struct ExchangeId(Guid Value) : IEntityId
{
    public ExchangeId()
        : this(GuidUtility.NewGuid()) { }
}
