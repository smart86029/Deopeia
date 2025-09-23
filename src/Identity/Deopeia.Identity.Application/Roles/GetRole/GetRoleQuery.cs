namespace Deopeia.Identity.Application.Roles.GetRole;

public sealed record GetRoleQuery(string Code) : IQuery<GetRoleResult>;
