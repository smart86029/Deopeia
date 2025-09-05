using Deopeia.Identity.Application.Authentication.SignIn;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Deopeia.Identity.Api.Pages.Authentication;

public class SignInModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

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
        var signInResult = await _mediator.Send(command);
        if (signInResult.UserId is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        if (signInResult.IsTwoFactorEnabled)
        {
            return RedirectToPage("TwoFactor", new { signInResult.UserId, ReturnUrl });
        }

        await SignInAsync(signInResult.UserId.Value);

        if (ReturnUrl.IsNullOrWhiteSpace())
        {
            return Redirect("~/");
        }

        return Redirect(ReturnUrl);
    }

    private async Task SignInAsync(Guid userId)
    {
        var claims = new Claim[] { new(ClaimTypes.NameIdentifier, userId.ToString()) };
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
    }
}
