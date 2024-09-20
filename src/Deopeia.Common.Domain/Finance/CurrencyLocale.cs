namespace Deopeia.Common.Domain.Finance;

public class CurrencyLocale : EntityLocale<CurrencyCode>
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
