namespace Deopeia.Identity.Application.Roles.GetRole;

public record GetRoleQuery(string Code) : IQuery<GetRoleResult>;
