using System.Security.Claims;

namespace Viriplaca.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var nameIdentifier = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = nameIdentifier?.ToGuid() ?? Guid.Empty;

        return result;
    }
}
