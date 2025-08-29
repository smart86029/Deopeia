namespace Deopeia.AdminPortal.Bff.Models.Permissions;

public sealed record GetRequest(bool? IsEnabled) : PagedRequest;
