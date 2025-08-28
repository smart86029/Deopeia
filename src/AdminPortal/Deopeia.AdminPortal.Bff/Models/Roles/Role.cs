namespace Deopeia.AdminPortal.Bff.Models.Roles;

public class Role
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public IReadOnlyList<string> RoleCodes { get; set; } = [];
}
