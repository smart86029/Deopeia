namespace Deopeia.AdminPortal.Bff.Models.Users;

public sealed record UserResponse(
    Guid Id,
    string UserName,
    bool IsEnabled,
    IReadOnlyList<string> RoleCodes
);
