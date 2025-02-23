namespace Deopeia.Identity.Application.Users.GetUser;

public class GetUserViewModel
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<string> RoleCodes { get; set; } = [];
}
