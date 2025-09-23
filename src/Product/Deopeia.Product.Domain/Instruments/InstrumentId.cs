namespace Deopeia.Product.Domain.Instruments;

public readonly record struct InstrumentId(Guid Guid) : IEntityId
{
    public InstrumentId()
        : this(Guid.CreateVersion7()) { }

    public override string ToString()
    {
        return Guid.ToString();
    }
}
