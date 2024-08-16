namespace Deopeia.Identity.Domain.Permissions;

public readonly record struct PermissionId(Guid Guid) : IEntityId
{
    public PermissionId()
        : this(GuidUtility.NewGuid()) { }
}
