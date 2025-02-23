using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Users;

public record UserRole(UserId UserId, RoleCode RoleCode) : ValueObject { }
