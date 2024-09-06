namespace Deopeia.Identity.Domain.Roles;

public class RoleLocale : EntityLocale<RoleId>
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }
}
