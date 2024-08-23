namespace Deopeia.Quote.Domain.Exchanges;

public readonly record struct ExchangeId(Guid Guid) : IEntityId
{
    public ExchangeId()
        : this(GuidUtility.NewGuid()) { }
}
