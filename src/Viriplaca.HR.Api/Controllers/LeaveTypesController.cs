using Viriplaca.HR.App.LeaveTypes.GetLeaveTypeOptions;

namespace Viriplaca.HR.Api.Controllers;

public class LeaveTypesController : ApiController<LeavesController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetLeaveTypeOptions([FromQuery] GetLeaveTypeOptionsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
