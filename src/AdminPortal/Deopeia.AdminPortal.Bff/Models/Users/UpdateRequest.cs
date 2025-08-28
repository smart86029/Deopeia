namespace Deopeia.AdminPortal.Bff.Models.Users;

public sealed record UpdateRequest(
    Guid Id,
    string UserName,
    string? Password,
    bool IsEnabled,
    List<string> RoleCodes
);
