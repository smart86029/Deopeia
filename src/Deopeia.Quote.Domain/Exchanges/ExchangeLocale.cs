namespace Deopeia.Quote.Domain.Exchanges;

public class ExchangeLocale : EntityLocale
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
