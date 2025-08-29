namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class OptionsController : ApiController
{
    private static readonly CultureInfo[] Parents = CultureInfo
        .GetCultures(CultureTypes.NeutralCultures)
        .Where(x => !x.Parent.Name.IsNullOrWhiteSpace())
        .Select(x => x.Parent)
        .Distinct()
        .ToArray();

    [HttpGet("Cultures")]
    public ActionResult<IReadOnlyList<OptionResponse<string>>> GetCultures()
    {
        return CultureInfo
            .GetCultures(CultureTypes.NeutralCultures)
            .Where(x => x.IsNeutralCulture && !Parents.Contains(x))
            .Select(x => new OptionResponse<string>(x.DisplayName, x.Name))
            .ToList();
    }
}
