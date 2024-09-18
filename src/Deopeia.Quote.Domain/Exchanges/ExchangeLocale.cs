namespace Deopeia.Quote.Domain.Exchanges;

public class ExchangeLocale : EntityLocale<ExchangeId>
{
    public string Name { get; private set; } = string.Empty;

    public string? Acronym { get; private set; }

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    public void UpdateAcronym(string? acronym)
    {
        Acronym = acronym?.Trim();
    }
}
