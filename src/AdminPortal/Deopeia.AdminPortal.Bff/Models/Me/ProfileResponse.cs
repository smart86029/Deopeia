namespace Deopeia.AdminPortal.Bff.Models.Me;

public sealed record ProfileResponse
{
    public string Name { get; init; } = string.Empty;

    public string? AvatarUrl { get; init; }
}
