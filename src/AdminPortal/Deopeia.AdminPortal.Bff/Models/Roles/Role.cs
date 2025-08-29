namespace Deopeia.AdminPortal.Bff.Models.Roles;

public sealed record Role(string Code, string Name, string? Description, bool IsEnabled);
