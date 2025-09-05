using Deopeia.Identity.Application.Authentication.VerifyTwoFactor;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Deopeia.Identity.Api.Pages.Authentication;

public class TwoFactorModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    [Required]
    [BindProperty]
    public string VerificationCode { get; set; } = string.Empty;

    [BindProperty]
    public Guid UserId { get; set; }

    [BindProperty]
    public string ReturnUrl { get; set; } = string.Empty;

    public IActionResult OnGet(Guid userId, string returnUrl)
    {
        UserId = userId;
        ReturnUrl = returnUrl;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new VerifyTwoFactorCommand(UserId, VerificationCode);
        var isValid = await _mediator.Send(command);
        if (!isValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid verification code");
            return Page();
        }

        await SignInAsync(UserId);

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
