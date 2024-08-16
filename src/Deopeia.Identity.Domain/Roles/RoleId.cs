namespace Deopeia.Identity.Domain.Roles;

public readonly record struct RoleId(Guid Guid) : IEntityId
{
    public RoleId()
        : this(GuidUtility.NewGuid()) { }
}
