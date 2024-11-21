namespace Deopeia.Quote.Domain.Companies;

public readonly record struct CompanyId(Guid Guid) : IEntityId
{
    public CompanyId()
        : this(Guid.CreateVersion7()) { }
}
