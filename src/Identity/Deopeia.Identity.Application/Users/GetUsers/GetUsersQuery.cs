namespace Deopeia.Identity.Application.Users.GetUsers;

public record GetUsersQuery(string? UserName, bool? IsEnabled, string? RoleCode)
    : PageQuery<UserDto> { }
