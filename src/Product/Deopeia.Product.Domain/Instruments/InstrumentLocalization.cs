namespace Deopeia.Product.Domain.Instruments;

public class InstrumentLocalization : EntityLocalization<InstrumentId>
{
    public string Name { get; private set; } = string.Empty;

    internal void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
