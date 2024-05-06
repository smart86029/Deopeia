using Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

namespace Viriplaca.HR.Api.Controllers;

public class LeaveEntitlementsController(CurrentUser currentUser)
    : ApiController<LeaveEntitlementsController>
{
    private readonly CurrentUser _currentUser = currentUser;

    [HttpGet]
    public async Task<IActionResult> GetLeaveEntitlements()
    {
        var command = new GetLeaveEntitlementsQuery(_currentUser.Id, DateTime.UtcNow.ToDateOnly());
        var results = await Sender.Send(command);

        return Ok(results);
    }
}
