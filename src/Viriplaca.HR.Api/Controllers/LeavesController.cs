using Viriplaca.HR.App.Leaves.GetLeaves;
using Viriplaca.HR.App.Leaves.GetLeaveTypes;

namespace Viriplaca.HR.Api.Controllers;

public class LeavesController : ApiController<LeavesController>
{
    [HttpGet("Types")]
    public async Task<IActionResult> GetLeaveTypes([FromQuery] GetLeaveTypesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaves([FromQuery] GetLeavesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
