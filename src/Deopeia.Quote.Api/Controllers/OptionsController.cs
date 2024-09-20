using Deopeia.Quote.Application.Options.GetCultures;
using Deopeia.Quote.Application.Options.GetCurrencies;
using Deopeia.Quote.Application.Options.GetTimeZones;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class OptionsController : ApiController<OptionsController>
{
    [HttpGet("Cultures")]
    public async Task<IActionResult> GetCultures()
    {
        var query = new GetCulturesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("Currencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        var query = new GetCurrenciesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("TimeZones")]
    public async Task<IActionResult> GetTimeZones()
    {
        var query = new GetTimeZonesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
