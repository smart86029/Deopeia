using Deopeia.Quote.Application.Options.GetTimeZones;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class OptionsController : ApiController<OptionsController>
{
    [HttpGet("TimeZones")]
    public async Task<IActionResult> GetTimeZones()
    {
        var query = new GetTimeZonesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
