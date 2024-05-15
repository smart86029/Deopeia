using Viriplaca.HR.App.Leaves.ApplyLeave;
using Viriplaca.HR.App.Leaves.CancelLeave;
using Viriplaca.HR.App.Leaves.GetLeave;
using Viriplaca.HR.App.Leaves.GetLeaves;
using Viriplaca.HR.App.Leaves.UpdateApprovalStatus;

namespace Viriplaca.HR.Api.Controllers;

public class LeavesController : ApiController<LeavesController>
{
    [HttpGet]
    public async Task<IActionResult> GetLeaves([FromQuery] GetLeavesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeave([FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetLeaveQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyLeave([FromBody] ApplyLeaveCommand command)
    {
        var result = await Sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelLeave([FromRoute] Guid id)
    {
        await Sender.Send(new CancelLeaveCommand(id));

        return Ok();
    }

    [HttpPut("{id}/ApprovalStatus")]
    public async Task<IActionResult> UpdateApprovalStatus(
        [FromRoute] Guid id,
        [FromBody] UpdateApprovalStatusCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return Ok();
    }
}
