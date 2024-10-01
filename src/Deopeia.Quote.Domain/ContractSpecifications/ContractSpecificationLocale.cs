namespace Deopeia.Quote.Domain.ContractSpecifications;

public class ContractSpecificationLocale : EntityLocale<ContractSpecificationId>
{
    public string Name { get; private set; } = string.Empty;

    public string NameTemplate { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    public void UpdateNameTemplate(string nameTemplate)
    {
        nameTemplate.MustNotBeNullOrWhiteSpace();
        NameTemplate = nameTemplate.Trim();
    }
}
