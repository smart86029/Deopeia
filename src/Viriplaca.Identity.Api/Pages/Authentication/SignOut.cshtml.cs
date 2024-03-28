using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pudicitia.Identity.Api.Pages.Authentication;

public class SignOutModel : PageModel
{
    public string SignOutId { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync([FromQuery] string signOutId)
    {
        SignOutId = signOutId;

        if (!User.Identity!.IsAuthenticated)
        {
            return await OnPostAsync();
        }

        //var context = await _interactionService.GetLogoutContextAsync(signOutId);
        //if (!context.ShowSignoutPrompt)
        //{
        //    return await OnPostAsync();
        //}

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (User.Identity!.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }

        //var context = await _interactionService.GetLogoutContextAsync(SignOutId);

        return Redirect(string.Empty);
    }
}
