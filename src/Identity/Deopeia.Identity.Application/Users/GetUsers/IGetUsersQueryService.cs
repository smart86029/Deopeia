namespace Deopeia.Identity.Application.Users.GetUsers;

public interface IGetUsersQueryService
{
    Task<PageResult<UserDto>> GetAsync(GetUsersQuery query);
}
