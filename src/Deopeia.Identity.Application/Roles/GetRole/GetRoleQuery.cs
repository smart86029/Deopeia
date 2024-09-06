namespace Deopeia.Identity.Application.Roles.GetRole;

public record GetRoleQuery(Guid Id) : IRequest<GetRoleViewModel> { }
