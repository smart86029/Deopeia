namespace Viriplaca.Identity.Domain.Permissions;

public class PermissionLocale : EntityLocale
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Name can not be null");
        }

        Name = name.Trim();
    }

    public void UpdateDescription(string description)
    {
        Description = description?.Trim();
    }
}
