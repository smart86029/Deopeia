namespace Deopeia.Identity.Application.Roles.GetRoles;

public interface IGetRolesQueryService
{
    Task<PagedResult<RoleDto>> GetAsync(GetRolesQuery query);
}
