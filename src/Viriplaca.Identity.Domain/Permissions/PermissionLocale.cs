namespace Viriplaca.Identity.Domain.Permissions;

public class PermissionLocale : EntityLocale
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    public void UpdateDescription(string description)
    {
        Description = description?.Trim();
    }
}
