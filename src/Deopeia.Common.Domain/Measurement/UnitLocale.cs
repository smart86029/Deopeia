namespace Deopeia.Common.Domain.Measurement;

public class UnitLocale : EntityLocale<UnitCode>
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
