using Viriplaca.HR.App.LeaveTypes.GetLeaveType;
using Viriplaca.HR.App.LeaveTypes.GetLeaveTypeOptions;
using Viriplaca.HR.App.LeaveTypes.GetLeaveTypes;
using Viriplaca.HR.App.LeaveTypes.UpdateLeaveType;

namespace Viriplaca.HR.Api.Controllers;

public class LeaveTypesController : ApiController<LeavesController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetLeaveTypeOptions([FromQuery] GetLeaveTypeOptionsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaveTypes([FromQuery] GetLeaveTypesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveType([FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetLeaveTypeQuery(id));

        return Ok(result);
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateLeaveType([FromBody] CreateLeaveTypeCommand command)
    //{
    //    var result = await Sender.Send(command);

    //    return Ok(result);
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLeaveType(
        [FromRoute] Guid id,
        [FromBody] UpdateLeaveTypeCommand command
    )
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);

        return Ok();
    }
}
