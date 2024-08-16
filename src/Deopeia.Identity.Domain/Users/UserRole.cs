using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Users;

public class UserRole(UserId userId, RoleId roleId)
    : Entity<UserRoleId>(new UserRoleId(userId, roleId))
{
    public UserId UserId => Id.UserId;

    public RoleId RoleId => Id.RoleId;
}
