namespace Deopeia.AdminPortal.Bff.Models.Users;

public record GetRequest : PagedRequest
{
    public string? UserName { get; init; }

    public bool? IsEnabled { get; init; }

    public string? RoleCode { get; init; }
}
