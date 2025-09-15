using System.Security.Claims;

namespace Deopeia.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var nameIdentifier = claimsPrincipal
            .Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;
        return nameIdentifier?.ToGuid() ?? Guid.Empty;
    }

    public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
    {
        var name = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        return name ?? string.Empty;
    }
}
