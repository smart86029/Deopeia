namespace Deopeia.Quote.Domain.Instruments;

public readonly record struct InstrumentId(Guid Guid) : IEntityId
{
    public InstrumentId()
        : this(GuidUtility.NewGuid()) { }
}
