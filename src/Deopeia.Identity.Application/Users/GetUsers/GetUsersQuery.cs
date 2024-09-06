namespace Deopeia.Identity.Application.Users.GetUsers;

public record GetUsersQuery(bool? IsEnabled) : PageQuery<UserDto> { }
