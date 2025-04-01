using Deopeia.Identity.Api.Services.Authentication;
using Deopeia.Identity.Application.Authentication.SignIn;

namespace Deopeia.Identity.Api.Pages.Authentication;

public class SignInModel(ISender sender, AuthenticationService authenticationService) : PageModel
{
    private readonly ISender _sender = sender;
    private readonly AuthenticationService _authenticationService = authenticationService;

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
        if (signInResult.UserId is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        if (signInResult.IsTwoFactorEnabled)
        {
            return RedirectToPage("TwoFactor", new { signInResult.UserId, ReturnUrl });
        }

        await _authenticationService.SignInAsync(signInResult.UserId.Value);

        if (ReturnUrl.IsNullOrWhiteSpace())
        {
            return Redirect("~/");
        }

        return Redirect(ReturnUrl);
    }
}
