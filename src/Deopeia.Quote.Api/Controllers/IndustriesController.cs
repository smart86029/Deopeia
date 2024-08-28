using Deopeia.Quote.Application.Industries.GetIndustryOptions;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class IndustriesController : ApiController<IndustriesController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions()
    {
        var query = new GetIndustryOptionsQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
