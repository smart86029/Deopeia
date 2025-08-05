namespace Deopeia.Identity.Domain.Permissions;

public readonly record struct PermissionCode(string Value) : IEntityId { }
