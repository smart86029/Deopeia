namespace Deopeia.Common.Domain.Finance;

public readonly record struct InstrumentId(Guid Guid) : IEntityId
{
    public InstrumentId()
        : this(GuidUtility.NewGuid()) { }
}
