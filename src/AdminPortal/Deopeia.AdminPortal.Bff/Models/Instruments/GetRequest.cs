namespace Deopeia.AdminPortal.Bff.Models.Instruments;

public sealed record GetRequest(string? Keyword, int? Type) : PagedRequest;
