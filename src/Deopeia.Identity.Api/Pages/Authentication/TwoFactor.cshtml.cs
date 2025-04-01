using Deopeia.Identity.Api.Services.Authentication;
using Deopeia.Identity.Application.Authentication.VerifyTwoFactor;

namespace Deopeia.Identity.Api.Pages.Authentication;

public class TwoFactorModel(ISender sender, AuthenticationService authenticationService) : PageModel
{
    private readonly ISender _sender = sender;
    private readonly AuthenticationService _authenticationService = authenticationService;

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
        var IsValid = await _sender.Send(command);
        if (!IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid verification code");
            return Page();
        }

        await _authenticationService.SignInAsync(UserId);

        if (ReturnUrl.IsNullOrWhiteSpace())
        {
            return Redirect("~/");
        }

        return Redirect(ReturnUrl);
    }
}
