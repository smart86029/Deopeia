namespace Deopeia.AdminPortal.Bff.Models.Roles;

public record GetRequest : PagedRequest
{
    public string? UserName { get; init; }

    public bool? IsEnabled { get; init; }

    public string? RoleCode { get; init; }
}
