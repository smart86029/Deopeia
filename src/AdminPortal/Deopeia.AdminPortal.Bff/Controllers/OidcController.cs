namespace Deopeia.AdminPortal.Bff.Controllers;

[Route("[controller]")]
public class OidcController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("SignIn")]
    public IActionResult SignIn(string? returnUrl)
    {
        var properties = GetProperties(returnUrl);
        return Challenge(properties);
    }

    private static AuthenticationProperties GetProperties(string? returnUrl)
    {
        // TODO: Use HttpContext.Request.PathBase instead.
        const string pathBase = "/";

        if (returnUrl.IsNullOrWhiteSpace())
        {
            returnUrl = pathBase;
        }
        else if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        {
            returnUrl = new Uri(returnUrl, UriKind.Absolute).PathAndQuery;
        }
        else if (returnUrl[0] != '/')
        {
            returnUrl = $"{pathBase}{returnUrl}";
        }

        return new AuthenticationProperties { RedirectUri = returnUrl };
    }
}
