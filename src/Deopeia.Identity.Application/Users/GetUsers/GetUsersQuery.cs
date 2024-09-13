namespace Deopeia.Identity.Application.Users.GetUsers;

public record GetUsersQuery(string? UserName, bool? IsEnabled, Guid? RoleId)
    : PageQuery<UserDto> { }
