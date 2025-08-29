namespace Deopeia.AdminPortal.Bff.Models.Users;

public sealed record GetRequest(string? UserName, bool? IsEnabled, string? RoleCode) : PagedRequest;
