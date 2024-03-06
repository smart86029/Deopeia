using Viriplaca.HR.App.Leaves.GetLeaves;

namespace Viriplaca.HR.Api.Controllers;

public class LeavesController : ApiController<LeavesController>
{
    [HttpGet]
    public async Task<IActionResult> GetLeaves([FromQuery] GetLeavesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
