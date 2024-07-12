namespace Deopeia.Quote.Domain.Companies;

public class CompanyLocale : EntityLocale
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
