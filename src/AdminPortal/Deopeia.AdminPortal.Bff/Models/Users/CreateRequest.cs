namespace Deopeia.AdminPortal.Bff.Models.Users;

public sealed record CreateRequest(
    string UserName,
    string Password,
    bool IsEnabled,
    List<string> RoleCodes
);
