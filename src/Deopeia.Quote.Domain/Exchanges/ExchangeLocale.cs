namespace Deopeia.Quote.Domain.Exchanges;

public class ExchangeLocale : EntityLocale<ExchangeId>
{
    public string Name { get; private set; } = string.Empty;

    public string? Abbreviation { get; private set; }

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    public void UpdateAbbreviation(string? abbreviation)
    {
        Abbreviation = abbreviation?.Trim();
    }
}
