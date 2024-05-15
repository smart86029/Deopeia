using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viriplaca.Identity.App.Authentication.SignIn;

namespace Viriplaca.Identity.Api.Pages.Authentication;

public class SignInModel(ISender sender) : PageModel
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

    public IActionResult OnGet(string returnUrl, string code)
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

        var command = new SignInCommand(UserName, Password, Code);
        var signInResult = await _sender.Send(command);
        if (signInResult.SubjectId.IsNullOrWhiteSpace())
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        var claims = new Claim[] { new(ClaimTypes.NameIdentifier, signInResult.SubjectId), };
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)),
        };
        await HttpContext.SignInAsync(principal, properties);

        if (ReturnUrl.IsNullOrWhiteSpace())
        {
            return Redirect("~/");
        }

        return Redirect(ReturnUrl);
    }
}
