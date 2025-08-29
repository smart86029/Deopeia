namespace Deopeia.AdminPortal.Bff.Models.Permissions;

public sealed record Permission(string Code, string Name, string? Description, bool IsEnabled);
