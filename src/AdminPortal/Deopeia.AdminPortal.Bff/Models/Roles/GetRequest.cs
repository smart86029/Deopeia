namespace Deopeia.AdminPortal.Bff.Models.Roles;

public sealed record GetRequest(bool? IsEnabled) : PagedRequest;
