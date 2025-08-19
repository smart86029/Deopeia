namespace Deopeia.Identity.Application.Users.GetUsers;

public interface IGetUsersQueryService
{
    Task<PagedResult<UserDto>> GetAsync(GetUsersQuery query);
}
