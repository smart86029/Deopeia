using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Viriplaca.Identity.App.Authentication.SignIn;
using Viriplaca.Identity.App.Connect.GenerateToken;

namespace Viriplaca.Identity.Api.Pages.Authentication;

public class SignInModel(ISender sender)
    : PageModel
{
    private readonly ISender _sender = sender;

    [Required]
    [BindProperty]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string Code { get; set; } = string.Empty;

    [BindProperty]
    public string ReturnUrl { get; set; } = string.Empty;

    public IActionResult OnGet(string code, string returnUrl)
    {
        Code = code;
        ReturnUrl = returnUrl;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new SignInCommand(UserName, Password);
        var signInResult = await _sender.Send(command);
        if (signInResult.SubjectId.IsNullOrWhiteSpace())
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, signInResult.SubjectId),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)),
        };
        await HttpContext.SignInAsync(principal, properties);

        var token = await _sender.Send(new GenerateTokenCommand(Code, principal));
        if (token is not null)
        {
            var url = $"{ReturnUrl}#{token.QueryString}";
            return Redirect(url);
        }

        if (Url.IsLocalUrl(ReturnUrl))
        {
            return Redirect(ReturnUrl);
        }

        if (ReturnUrl.IsNullOrWhiteSpace())
        {
            return Redirect("~/");
        }

        throw new Exception("Invalid return URL");
    }
}
