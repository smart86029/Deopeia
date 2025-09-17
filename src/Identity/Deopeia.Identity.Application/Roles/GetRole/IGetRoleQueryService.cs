namespace Deopeia.Identity.Application.Roles.GetRole;

public interface IGetRoleQueryService
{
    Task<GetRoleResult> GetAsync(GetRoleQuery query);
}
