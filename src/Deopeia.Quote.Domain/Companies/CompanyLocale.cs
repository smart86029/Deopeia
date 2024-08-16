namespace Deopeia.Quote.Domain.Companies;

public class CompanyLocale : EntityLocale<CompanyId>
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
