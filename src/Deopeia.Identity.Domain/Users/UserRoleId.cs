using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Users;

public readonly record struct UserRoleId(UserId UserId, RoleId RoleId) : IEntityId { }
