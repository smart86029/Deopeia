namespace Deopeia.Identity.Domain.Grants;

public readonly record struct GrantId(Guid Guid) : IEntityId
{
    public GrantId()
        : this(GuidUtility.NewGuid()) { }
}
