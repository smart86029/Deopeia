namespace Deopeia.Quote.Domain.Companies;

public class Company : AggregateRoot<CompanyId>, ILocalizable<CompanyLocale, CompanyId>
{
    private readonly EntityLocaleCollection<CompanyLocale, CompanyId> _locales = [];

    private Company() { }

    public Company(SubIndustry subIndustry, Uri? website)
    {
        SubIndustry = subIndustry;
        Website = website;
    }

    public SubIndustry SubIndustry { get; set; }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public Uri? Website { get; private set; }

    public IReadOnlyCollection<CompanyLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }
}
