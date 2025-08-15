namespace Deopeia.Identity.Application.Users.GetUsers;

public sealed record GetUsersQuery(string? UserName, bool? IsEnabled, string? RoleCode)
    : PageQuery<UserDto> { }
