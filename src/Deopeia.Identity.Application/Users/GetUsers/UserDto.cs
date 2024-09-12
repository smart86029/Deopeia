namespace Deopeia.Identity.Application.Users.GetUsers;

public class UserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<Guid> RoleIds { get; set; } = [];
}
