namespace Deopeia.Identity.Application.Roles.GetRole;

public interface IGetRoleQueryService
{
    Task<GetRoleViewModel> GetAsync(GetRoleQuery query);
}
